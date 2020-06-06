using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;
using Network.Packets;
using DisruptorUnity3d;
using ENet;
using MessagePack;
using UI;
using UnityEngine.Serialization;
using utils;
using Debug = UnityEngine.Debug;
using Event = ENet.Event;
using EventType = ENet.EventType;

public class GameServerComm : Singleton<GameServerComm>
{
    private Address address;
    private BlockingCollection<DefaultPacket> _ringQueue;
    public Peer serverPeer;
    public bool isGSset;
    private Thread _eventThread;
    private GameLogic _gameLogic;
    
    protected GameServerComm () {}
    
    void Awake ()
    {
        Debug.Log("[Awake] GameServerComm Instanciated");
        address = new Address();

        _gameLogic = GameLogic.Instance;
        isGSset = false;
    }

    public void SetAndRunServerHost(string gameServerIp, ushort port)
    {
        Debug.Log("[Connecting] Proxy Game Server");
        _ringQueue = _gameLogic._ringQueue;
        
        address.SetHost(gameServerIp);
        address.Port = port;
        
        _eventThread = new Thread(Launch);
        
        //_eventThread.IsBackground = true;
        
        _eventThread.Start();
    }

    public void Launch()
    {
        byte[] receivedBytes = new byte[Globals.CAPACITY*10];
        
        using (Host client = new Host())
        {
            client.Create();
            serverPeer = client.Connect(address);
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
                            Debug.Log("[Unity] Connected to Game Server (" + netEvent.Peer.IP + ")");
                            isGSset = true;
                            break;

                        case EventType.Disconnect:
                            Debug.Log("[Unity] Disconnected from Game Server (" + netEvent.Peer.IP + ")");
                            break;

                        case EventType.Timeout:
                            Debug.Log("[Unity] Game Server (" + netEvent.Peer.IP + ") TIMEOUT");
                            break;

                        case EventType.Receive:
                            
                            netEvent.Packet.CopyTo(receivedBytes);
                            DefaultPacket receivedPacket = MessagePackSerializer.Deserialize<DefaultPacket>(receivedBytes);
                            
                            /*Debug.Log("Packet received from - ID: " + netEvent.Peer.ID + ", IP: " +
                                      netEvent.Peer.IP + ", Channel ID: " + netEvent.ChannelID +
                                      ", Data length: " + netEvent.Packet.Length + ", Type: " + receivedPacket.PacketType);*/

                            _ringQueue.Add(receivedPacket);

                            if (receivedPacket.PacketType == PacketType.END_COMM)
                            {
                                Debug.Log("End Game Server communication");
                                netEvent.Packet.Dispose();
                                return;
                            }
                            
                            netEvent.Packet.Dispose();
                            break;
                    }
                }
            }
        }
    }
}
