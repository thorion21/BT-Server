using System;
using ENet;
using BT_GameServer.utils;

namespace BT_GameServer
{
    public class WorldServerChannelCommunicator
    {
        private Address address;
        public WorldServerChannelCommunicator()
        {
            address = new Address();
            address.SetHost(Globals.INTERNAL_WORLD_SERVER_ADDR);
            address.Port = Globals.INTERNAL_WORLD_SERVER_PORT;
        }
        public void Launch()
        {
            using (Host client = new Host())
            {
                client.Create();
                Peer peer = client.Connect(address);

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
                                Console.WriteLine("[GameServer] Connected to World Server (" + netEvent.Peer.IP + ")");
                                break;

                            case EventType.Disconnect:
                                Console.WriteLine("[GameServer] Disconnected from World Server (" + netEvent.Peer.IP + ")");
                                break;

                            case EventType.Timeout:
                                Console.WriteLine("[GameServer] World Server (" + netEvent.Peer.IP + ") TIMEOUT");
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

        ~WorldServerChannelCommunicator()
        {
            Console.WriteLine("[GameServer] Stopping WorldServer Communicator...");
        }
    }
}