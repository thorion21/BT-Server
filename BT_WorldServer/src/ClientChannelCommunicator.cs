using System;
using System.Diagnostics;
using BT_WorldServer.libs.Disruptor;
using BT_WorldServer.Packets;
using ENet;
using BT_WorldServer.utils;
using MessagePack;

namespace BT_WorldServer
{
    public class ClientChannelCommunicator
    {
        private Address address;
        private RingBuffer<DefaultPacket> _worldQueue;
        private RingBuffer<DefaultPacket> _sendingQueue;
        
        public ClientChannelCommunicator(
            ref RingBuffer<DefaultPacket> worldQueue,
            ref RingBuffer<DefaultPacket> sendingQueue
            )
        {
            address = new Address();
            address.SetIP(Globals.WORLD_SERVER_ADDR);
            address.Port = Globals.WORLD_SERVER_PORT;
            _worldQueue = worldQueue;
            _sendingQueue = sendingQueue;
        }
        public void Launch()
        {
            byte[] receivedBytes = new byte[Globals.CAPACITY];
            
            using (Host server = new Host())
            {
                server.Create(address, Globals.MAX_CLIENTS);

                while (true)
                {
                    Event netEvent;

                    while (server.Service(0, out netEvent) > 0)
                    {
                        switch (netEvent.Type)
                        {
                            case EventType.None:
                                break;

                            case EventType.Connect:
                                Console.WriteLine("Client connected - ID: " + netEvent.Peer.ID + ", IP: " +
                                                  netEvent.Peer.IP);
                                break;

                            case EventType.Disconnect:
                                Console.WriteLine("Client disconnected - ID: " + netEvent.Peer.ID + ", IP: " +
                                                  netEvent.Peer.IP);
                                break;

                            case EventType.Timeout:
                                Console.WriteLine("Client timeout - ID: " + netEvent.Peer.ID + ", IP: " +
                                                  netEvent.Peer.IP);
                                break;

                            case EventType.Receive:
                                Console.WriteLine("Packet received from - ID: " + netEvent.Peer.ID + ", IP: " +
                                                  netEvent.Peer.IP + ", Channel ID: " + netEvent.ChannelID +
                                                  ", Data length: " + netEvent.Packet.Length);
                                
                                netEvent.Packet.CopyTo(receivedBytes);
                                DefaultPacket receivedPacket = MessagePackSerializer.Deserialize<DefaultPacket>(receivedBytes);
                                receivedPacket.SetPeer(netEvent.Peer);
                                _worldQueue.Enqueue(receivedPacket);
                                
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
                        /*packet.Create(toSendPacket.AsByteArray());
                        toSendPacket.Peer.Send(Globals.DEFAULT_CHANNEL, ref packet);*/
                    }
                
                    stopwatch.Restart();   
                }
            }
        }

        ~ClientChannelCommunicator()
        {
            Console.WriteLine("[WorldServer] Stopping Client Communicator...");
        }
    }
}