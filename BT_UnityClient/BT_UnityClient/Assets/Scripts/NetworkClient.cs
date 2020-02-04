using System;
using System.Diagnostics;
using System.Threading;
using Network.Packets;
using DisruptorUnity3d;
using ENet;
using MessagePack;
using utils;
using Debug = UnityEngine.Debug;
using Event = ENet.Event;
using EventType = ENet.EventType;

public class NetworkClient : Singleton<NetworkClient>
{
    private Address address;
    private RingBuffer<DefaultPacket> _ringQueue;
    private RingBuffer<DefaultPacket> _sendingQueue;
    private Peer _serverPeer;
    private Thread _eventThread, _sendingThread;
    private GameLogic _gameLogic;
    
    protected NetworkClient () {}
    
    void Awake ()
    {
        Debug.Log("[Awake] Network Client Instanciated");
        address = new Address();
        address.SetHost(Globals.WORLD_SERVER_ADDR);
        address.Port = Globals.WORLD_SERVER_PORT;

        _ringQueue = new RingBuffer<DefaultPacket>(Globals.MAX_RING_SIZE);
        _sendingQueue = new RingBuffer<DefaultPacket>(Globals.MAX_RING_SIZE);
        
        _gameLogic = GameLogic.Instance;
        _gameLogic.SetRingQueue(ref _ringQueue);
        _gameLogic.SetSendingQueue(ref _sendingQueue);
        
        _eventThread = new Thread(Launch);
        _sendingThread = new Thread(PacketSenderWorker);
        
        _eventThread.Start();
        _sendingThread.Start();
    }

    public void Launch()
    {
        byte[] receivedBytes = new byte[Globals.CAPACITY];
        
        using (Host client = new Host())
        {
            client.Create();
            _serverPeer = client.Connect(address);
            Debug.Log("Trying to connect to " + Globals.WORLD_SERVER_ADDR + "...");

            while (true)
            {
                Event netEvent;

                while (client.Service(0, out netEvent) > 0)
                {
                    switch (netEvent.Type)
                    {
                        case EventType.None:
                            break;
                        
                        case EventType.Connect:
                            Debug.Log("[Unity] Connected to World Server (" + netEvent.Peer.IP + ")");
                            break;

                        case EventType.Disconnect:
                            Debug.Log("[Unity] Disconnected from World Server (" + netEvent.Peer.IP + ")");
                            break;

                        case EventType.Timeout:
                            Debug.Log("[Unity] World Server (" + netEvent.Peer.IP + ") TIMEOUT");
                            break;

                        case EventType.Receive:
                            Debug.Log("Packet received from - ID: " + netEvent.Peer.ID + ", IP: " +
                                      netEvent.Peer.IP + ", Channel ID: " + netEvent.ChannelID +
                                      ", Data length: " + netEvent.Packet.Length);
                            
                            netEvent.Packet.CopyTo(receivedBytes);
                            DefaultPacket receivedPacket = MessagePackSerializer.Deserialize<DefaultPacket>(receivedBytes);
                            _ringQueue.Enqueue(receivedPacket);
                            
                            netEvent.Packet.Dispose();
                            break;
                    }
                }
            }
        }
    }
    
    public void PacketSenderWorker()
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
            
        while (true)
        {
            if (stopwatch.ElapsedMilliseconds >= Globals.TICK_TIME)
            {
                Console.Write("Tick! ");
                while (_sendingQueue.TryDequeue(out var toSendPacket))
                {
                    Packet packet = default(Packet);
                    packet.Create(toSendPacket.AsByteArray());
                    _serverPeer.Send(Globals.DEFAULT_CHANNEL, ref packet);
                }
                
                stopwatch.Restart();   
            }
        }
    }
}
