using System;
using ENet;
using BT_Server.utils;

namespace BT_Server
{
    public class ENetBase
    {
        private Address address;
        public ENetBase()
        {
            address = new Address();
            address.Port = Globals.PORT;
            Console.WriteLine("Initializing ENet.");
            ENet.Library.Initialize();
        }
        public void Launch()
        {
            Console.WriteLine("Launching Server...");
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
                                netEvent.Packet.Dispose();
                                break;
                        }
                    }
                }
            }
        }

        ~ENetBase()
        {
            Console.WriteLine("Stopping Server...");
            Console.WriteLine("Deinitializing ENet.");
            ENet.Library.Deinitialize();
        }
    }
}