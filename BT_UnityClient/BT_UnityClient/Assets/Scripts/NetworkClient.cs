﻿using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;
using Network.Packets;
using DisruptorUnity3d;
using ENet;
using GameScripts;
using MessagePack;
using UI;
using UnityEngine;
using utils;
using Debug = UnityEngine.Debug;
using Event = ENet.Event;
using EventType = ENet.EventType;

public class NetworkClient : Singleton<NetworkClient>
{
    private Address address;
    private BlockingCollection<DefaultPacket> _ringQueue;
    private BlockingCollection<DefaultPacket> _sendingQueue;
    private BlockingCollection<DefaultPacket> _gsSendingQueue;
    private Peer _serverPeer;
    private Thread _eventThread, _sendingThread;
    private GameLogic _gameLogic;
    private GameServerComm _gs;
    private ClientSidePrediction _csp;
    
    protected NetworkClient () {}
    
    void Awake ()
    {
        Debug.Log("[Awake] Network Client Instanciated");
        address = new Address();
        address.SetHost(Globals.WORLD_SERVER_ADDR);
        address.Port = Globals.WORLD_SERVER_PORT;

        _ringQueue = new BlockingCollection<DefaultPacket>(Globals.MAX_RING_SIZE);
        _sendingQueue = new BlockingCollection<DefaultPacket>(Globals.MAX_RING_SIZE);
        _gsSendingQueue = new BlockingCollection<DefaultPacket>(Globals.MAX_RING_SIZE);

        _csp = ClientSidePrediction.Instance;
        
        _gs = GameServerComm.Instance;
        _gameLogic = GameLogic.Instance;
        _gameLogic.SetDataQueuesAndLaunch(ref _ringQueue, ref _sendingQueue, ref _gsSendingQueue);
        
        _eventThread = new Thread(Launch);
        _sendingThread = new Thread(PacketSenderWorker);
        
        _eventThread.Start();
        _sendingThread.Start();
    }

    public void Launch()
    {
        byte[] receivedBytes = new byte[Globals.CAPACITY*10];
        
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
                            try
                            {
                                netEvent.Packet.CopyTo(receivedBytes);
                            }
                            catch (Exception e)
                            {
                                Debug.Log("Length: " + netEvent.Packet.Length);
                                Debug.Log("Data: " + netEvent.Packet.Data);
                                Debug.Log("IsSet: " + netEvent.Packet.IsSet);
                                Debug.LogError(e);
                            }
                            
                            DefaultPacket receivedPacket = MessagePackSerializer.Deserialize<DefaultPacket>(receivedBytes);
                            _ringQueue.Add(receivedPacket);
                            
                            Debug.Log("Packet received from - ID: " + netEvent.Peer.ID + ", IP: " +
                                      netEvent.Peer.IP + ", Channel ID: " + netEvent.ChannelID +
                                      ", Data length: " + netEvent.Packet.Length + ", Type: " + receivedPacket.PacketType);
                            
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
                /* For World Server */
                while (_sendingQueue.TryTake(out var toSendPacket))
                {
                    Packet packet = default(Packet);
                    packet.Create(toSendPacket.AsByteArray());
                    _serverPeer.Send(Globals.DEFAULT_CHANNEL, ref packet);
                }
                
                /* For Game Server */
                if (_gs.isGSset)
                {
                    if (_csp.IsFinalStatePacketReady())
                    {
                        //Debug.Log("Sending input packet");
                        Packet packet = default(Packet);
                        packet.Create(_csp.GetFinalStatePacket().AsByteArray(), PacketFlags.None);
                        _gs.serverPeer.Send(Globals.DEFAULT_CHANNEL, ref packet);
                    }
                    
                    while (_gsSendingQueue.TryTake(out var toSendPacket))
                    {
                        //Debug.Log("IN network client GS packet sent -> " + toSendPacket.PacketType);
                        Packet packet = default(Packet);
                        packet.Create(toSendPacket.AsByteArray(), PacketFlags.Reliable);
                        _gs.serverPeer.Send(Globals.DEFAULT_CHANNEL, ref packet);
                    }
                }
                
                stopwatch.Restart();   
            }
        }
    }
}
