using System;
using ENet;
using BT_WorldServer.utils;

namespace BT_WorldServer
{
    public class ClientChannelCommunicator
    {
        private Address address;
        public ClientChannelCommunicator()
        {
            address = new Address();
            address.SetIP(Globals.WORLD_SERVER_ADDR);
            address.Port = Globals.WORLD_SERVER_PORT;
        }
        public void Launch()
        {
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

        ~ClientChannelCommunicator()
        {
            Console.WriteLine("[WorldServer] Stopping Client Communicator...");
        }
    }
}