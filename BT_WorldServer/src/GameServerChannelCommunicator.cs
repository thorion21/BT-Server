using System;
using ENet;
using BT_WorldServer.utils;

namespace BT_WorldServer
{
    public class GameServerChannelCommunicator
    {
        private Address address;
        public GameServerChannelCommunicator()
        {
            address = new Address();
            address.SetIP(Globals.INTERNAL_GAME_SERVER_ADDR);
            address.Port = Globals.INTERNAL_GAME_SERVER_PORT;
        }
        public void Launch()
        {
            using (Host server = new Host())
            {
                server.Create(address, 1);

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
                                Console.WriteLine("[WorldServer] GameServer (" + netEvent.Peer.IP + ") connected.");
                                break;

                            case EventType.Disconnect:
                                Console.WriteLine("[WorldServer] GameServer (" + netEvent.Peer.IP + ") disconnected.");
                                break;

                            case EventType.Timeout:
                                Console.WriteLine("[WorldServer] GameServer (" + netEvent.Peer.IP + ") timed out");
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

        ~GameServerChannelCommunicator()
        {
            Console.WriteLine("[WorldServer] Stopping GameServer Communicator...");
        }
    }
}