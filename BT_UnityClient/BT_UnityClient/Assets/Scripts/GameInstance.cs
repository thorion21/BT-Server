using System;
using System.Collections.Generic;
using Account;
using DisruptorUnity3d;
using GameScripts;
using Network.Packets;
using Network.Packets.Instance;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using utils;

public class GameInstance : Singleton<NetworkClient>
{
    private Dictionary<string, PacketPlayer> _players;
    private Dictionary<string, BotMovement> _capsules;
    private RingBuffer<Tuple<byte, object>> _events;
    private ClientSidePrediction _cspScript;
    private string _myIgn;
    
    public GameObject cspObj;
    public GameObject playerCapsulePrefab;
    public int health;

    protected GameInstance () {}

    void Awake()
    {
        _events = new RingBuffer<Tuple<byte, object>>(Globals.GAME_EVENTS_LIMIT);
        _players = new Dictionary<string, PacketPlayer>();
        _capsules = new Dictionary<string, BotMovement>();
        _cspScript = cspObj.GetComponent<ClientSidePrediction>();
        health = 100;
    }

    public void Signal(byte @event, object obj = null)
    {
        _events.Enqueue(new Tuple<byte, object>(@event, obj));
    }

    void Update()
    {
        while (_events.TryDequeue(out var @event))
        {
            switch (@event.Item1)
            {
                case GameEvents.SpawnPlayers:
                    SpawnPlayers(@event.Item2);
                    break;
                case GameEvents.NotifyName:
                    NotifyName(@event.Item2);
                    break;
                case GameEvents.Correction:
                    _cspScript.AddStateCorrectionMessage(@event.Item2);
                    break;
                case GameEvents.NotifyInstance:
                    NotifyInstance(@event.Item2);
                    break;
                case GameEvents.WorldState:
                    WorldStateUpdate(@event.Item2);
                    break;
                case GameEvents.HealthUpdate:
                    UpdateHealth(@event.Item2);
                    break;
                default:
                    Debug.Log("missed packet? " + @event.Item1);
                    break;
            }
        }
    }

    private void NotifyName(object value)
    {
        _myIgn = (string) value;
    }

    private void NotifyInstance(object obj)
    {
        int instance_id = (int) obj;
        _cspScript.SetInstance(instance_id);
    }
    
    private void SpawnPlayers(object players)
    {
        foreach (var player in (List<PacketPlayer>) players)
        {
            _players.Add(player.Ign, player);
            GameObject playerObj = Instantiate(playerCapsulePrefab, new Vector3(player.position.x, player.position.y + 1.0f, player.position.z), Quaternion.identity);
            playerObj.transform.SetParent(transform);
            
            if (player.Ign == _myIgn)
            {
                _cspScript.SetMainPlayer(playerObj);
                Camera.main.transform.SetParent(playerObj.transform);
                Camera.main.transform.position = new Vector3(0f, 1.75f, 0f);
                playerObj.AddComponent<PlayerFirstPersonLook>();
                
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                BoxCollider bcoll = cube.GetComponent<BoxCollider>();
                Destroy(bcoll);
                
                var cubeRenderer = cube.GetComponent<Renderer>();
                
                cubeRenderer.material.SetColor("_Color", Color.black);
                
                cube.transform.SetParent(Camera.main.transform);
                cube.transform.localPosition = new Vector3(0.425f, -0.376f, 0.737f);
                
                cube.transform.localRotation = Quaternion.identity;
                cube.transform.localScale = new Vector3(0.15054f, 0.18071f, 1f);
            }
            else
            {
                var text3d = Get3DText(player.Ign);
                text3d.transform.SetParent(playerObj.transform);
                text3d.transform.position = new Vector3(0f, 2.25f, 0f);
                text3d.transform.localScale = new Vector3(0.05f, 0.05f, 0);
                text3d.transform.localRotation = Quaternion.identity;
                BotMovement bot_script = playerObj.AddComponent<BotMovement>();
                _capsules.Add(player.Ign, bot_script);
            }
            
            
            
        }
    }
    
    private GameObject Get3DText(string name)
    {
        var theText = new GameObject();
        
        var textMesh = theText.AddComponent<TextMesh>();
        var meshRenderer = theText.AddComponent<MeshRenderer>();

        textMesh.text = name;
        textMesh.anchor = TextAnchor.MiddleCenter;
        textMesh.alignment = TextAlignment.Center;
        textMesh.fontSize = 128;

        theText.AddComponent<NameFollow>();
        return theText;
    }
    
    private Vector3 ToVector3(NetworkVector v)
    {
        return new Vector3(v.x, v.y, v.z);
    }

    private void WorldStateUpdate(object obj)
    {
        (string, NetworkVector) entry = ((string, NetworkVector)) obj;
        _capsules[entry.Item1].destination = ToVector3(entry.Item2);
        Debug.Log("Update position for " + entry.Item1);
    }

    private void UpdateHealth(object obj)
    {
        health = (int) obj;
        Debug.Log("================>>>>>> Updated HEALTH " + health);
    }
    
}