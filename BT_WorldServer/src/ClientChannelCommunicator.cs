using System;
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
        private RingBuffer<DefaultPacket> ringBuffer;
        
        public ClientChannelCommunicator(ref RingBuffer<DefaultPacket> rQueue)
        {
            address = new Address();
            address.SetIP(Globals.WORLD_SERVER_ADDR);
            address.Port = Globals.WORLD_SERVER_PORT;
            ringBuffer = rQueue;
        }
        public void Launch()
        {
            byte[] toSendBytes = new byte[Globals.CAPACITY];
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
                                
                                Packet packet = default(Packet);
                                byte[] data = new byte[64];

                                packet.Create(data);
                                netEvent.Peer.Send(netEvent.ChannelID, ref packet);

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
                                ringBuffer.Enqueue(receivedPacket);
                                
                                netEvent.Packet.Dispose();
                                break;
                        }
                    }
                }
            }
        }

        ~ClientChannelCommunicator()
        {
            Console.WriteLine("[WorldServer] Stopping Client Communicator...");
        }
    }
}