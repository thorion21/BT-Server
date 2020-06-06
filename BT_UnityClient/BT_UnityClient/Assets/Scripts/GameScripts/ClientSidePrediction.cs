using System;
using System.Collections.Concurrent;
using System.Net;
using Account;
using DisruptorUnity3d;
using JetBrains.Annotations;
using Network.Packets;
using Network.Packets.Game;
using Network.Packets.Instance;
using UnityEngine;
using utils;

namespace GameScripts
{
    public class ClientSidePrediction : Singleton<ClientSidePrediction>
    {
        private GameObject _mainPlayer;
        private uint client_tick;
        private uint last_recieved_client_tick;
        private float client_timer;
        private const int client_buffer_size = 1024;
        private GameLogic _gameLogic;
        private Inputs[] input_history;
        private Vector3[] position_history;
        private Vector3[] rotation_history;
        private float tick_time;
        private Rigidbody rb;
        [Range(0.0f, 5000.0f)] public float speed = 10f;
        private MyAccount _account;
        private int instance_id;

        private bool _finalStateReady = false;
        private DefaultPacket _finalStatePacket;

        private BlockingCollection<GSCorrectionsPacket> _serverCorrectionsQueue;
        
        protected ClientSidePrediction() {}

        private void Awake()
        {
            _account = MyAccount.Instance;
            //tick_time = 1.0f / Globals.TICK_RATE;
            client_tick = 0;
            client_timer = 0;
            _gameLogic = GameLogic.Instance;
            input_history = new Inputs[client_buffer_size];
            position_history = new Vector3[client_buffer_size];
            rotation_history = new Vector3[client_buffer_size];
            _serverCorrectionsQueue = new BlockingCollection<GSCorrectionsPacket>();
            last_recieved_client_tick = 0;
        }

        void Update()
        {
            if (_mainPlayer == null)
                return;
            
            rb = _mainPlayer.GetComponent<Rigidbody>();
            tick_time = Time.fixedDeltaTime;
            
            client_timer += Time.deltaTime;
            while (client_timer >= tick_time)
            {
                client_timer -= tick_time;

                uint buffer_slot = client_tick % client_buffer_size;

                Inputs inputs;
                inputs.left = Input.GetKey(KeyCode.A);
                inputs.right = Input.GetKey(KeyCode.D);
                inputs.forward = Input.GetKey(KeyCode.W);
                inputs.backward = Input.GetKey(KeyCode.S);
                inputs.fire = Input.GetMouseButton(0);

                //Debug.Log("A(" + inputs.left + ") D(" + inputs.right + ") W(" + inputs.forward + ") S(" + inputs.backward + ") Tick(" + _clientTickNumber + ")");
                
                input_history[buffer_slot] = inputs;
                
                // Store state for this tick
                StoreState(buffer_slot);
                
                // Send packet to server
                SendToServer(inputs, client_tick);
                
                // Simulate physics client side
                SimulatePhysics(inputs);
                
                // Update client tick
                client_tick++;
            }

            GSCorrectionsPacket server_state;
            
            if (_serverCorrectionsQueue.TryTake(out server_state))
            {
                while (true)
                {
                    bool m_Success = _serverCorrectionsQueue.TryTake(out var new_server_state);

                    if (m_Success && new_server_state.tick_number >= last_recieved_client_tick)
                        server_state = new_server_state;
                    else
                        break;
                }
                
                Vector3 pos_error = _mainPlayer.transform.position - ToVector3(server_state.position);
                last_recieved_client_tick = server_state.tick_number;
                
                if (pos_error.sqrMagnitude > 1e-2)
                {
                    // rewind & replay

                    _mainPlayer.transform.position = ToVector3(server_state.position);

                    Physics.autoSimulation = false;
                    
                    for (uint i = server_state.tick_number; i < client_tick; i++)
                    {
                        uint buffer_slot = i % client_buffer_size;
                        position_history[buffer_slot] = _mainPlayer.transform.position;
                        rotation_history[buffer_slot] = rb.rotation.eulerAngles;
                        
                        SimulatePhysics(input_history[buffer_slot]);
                        Physics.Simulate(Time.fixedDeltaTime);
                    }

                    Physics.autoSimulation = true;
                    Debug.Log("Applying correction for tick (" + server_state.tick_number + ") new position is (" + _mainPlayer.transform.position + ") and error was " + pos_error);
                }
            }
        }
        
        public void SetMainPlayer(GameObject obj)
        {
            _mainPlayer = obj;
        }

        public void SetInstance(int value)
        {
            instance_id = value;
        }

        public void AddStateCorrectionMessage(object state)
        {
            _serverCorrectionsQueue.Add((GSCorrectionsPacket) state);
        }

        public bool IsFinalStatePacketReady()
        {
            return _finalStateReady;
        }
        
        public DefaultPacket GetFinalStatePacket()
        {
            _finalStateReady = false;
            return _finalStatePacket;
        }

        private NetworkVector GetPlayerPos()
        {
            var position = rb.position;
            return new NetworkVector(position.x, position.y, position.z);
        }

        private NetworkVector GetPlayerRotation()
        {
            var rot = rb.rotation.eulerAngles;
            return new NetworkVector(rot.x, rot.y, rot.z);
        }

        private Vector3 ToVector3(NetworkVector v)
        {
            return new Vector3(v.x, v.y, v.z);
        }

        private void SendToServer(Inputs inputs, uint tickNumber)
        {
            if (inputs.forward || inputs.left || inputs.right || inputs.backward || inputs.fire)
            {
                DefaultPacket packet = new DefaultPacket(
                    PacketType.INPUTS_PKT,
                    new InputsPacket(
                        inputs.left, inputs.right, inputs.forward, inputs.backward, inputs.fire, tickNumber,
                        _account.GetIGN(), instance_id, GetPlayerRotation(), Camera.main.transform.eulerAngles.x
                    ).AsByteArray()
                );

                _finalStateReady = true;
                _finalStatePacket = packet;
            }
        }

        public void SimulatePhysics(Inputs inputs)
        {
            Vector3 movement = new Vector3(0f, 0f, 0f);
            var factor = speed * tick_time;

            if (inputs.left)
                movement += new Vector3(-factor, 0f, 0f);
            if (inputs.right)
                movement += new Vector3(factor, 0f, 0f);
            if (inputs.forward)
                movement += new Vector3(0f, 0f, factor);
            if (inputs.backward)
                movement += new Vector3(0f, 0f, -factor);
            
            var mv = _mainPlayer.transform.right * movement.x + _mainPlayer.transform.forward * movement.z;
            //mv.y = movement.y;
            
            /*var rot = rb.rotation.eulerAngles;
            
            float xPos = Mathf.Sin(rot.y * Mathf.Deg2Rad) * Mathf.Cos(rot.x * Mathf.Deg2Rad);
            float yPos = Mathf.Sin(-rot.x * Mathf.Deg2Rad);
            float zPos = Mathf.Cos(rot.x * Mathf.Deg2Rad) * Mathf.Cos(rot.y * Mathf.Deg2Rad);
            
            Vector3 myForward = new Vector3(xPos, yPos, zPos);
            Vector3 myRight = new Vector3(zPos, yPos, -xPos);
            
            var mv = myRight * movement.x + myForward * movement.z;
            mv.y = movement.y;
            
            //rb.velocity = mv;*/
            _mainPlayer.transform.position += mv;

        }

        private void StoreState(uint buffer_slot)
        {
            position_history[buffer_slot] = rb.position;
            rotation_history[buffer_slot] = rb.rotation.eulerAngles;
        }
    }
}