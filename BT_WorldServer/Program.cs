/*
 * Doesn't have a GameTick
 * Purpose:
 *     1. Authenticate users
 *     2. Keeps a list of logged players and their info
 *     3. Once a game starts, it needs to send the player data about the GameServer data (IP, Port)
 *     4. After a game ends, writes data to database
 *     5. Processes menu actions only - like creating room, adding friends, inventory interrogation etc.
 *     6. Communicates with the GameServer constantly to supply room data and to calculate end match rewards
 */

using System.Threading;
using BT_WorldServer.Packets;
using BT_WorldServer.utils;
using BT_WorldServer.libs.Disruptor;

namespace BT_WorldServer
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            /*GenericPlayer own = new GenericPlayer("gogu", "abs");
            Room r = new Room(GameModes.GM_SND, Map.MAP_DEFAULT, 12, own);

            r.AsByteArray();
            DefaultPacket dp = PacketFactory.Build(0x7, r);

            byte[] bytes = dp.AsByteArray();
            
            Console.WriteLine("Bytes in message: " + bytes.Length);
            
            var json = MessagePackSerializer.ConvertToJson(bytes);
            Console.WriteLine(json);

            DefaultPacket receivedPacket = MessagePackSerializer.Deserialize<DefaultPacket>(bytes);
            Console.WriteLine("Rcv packet:");
            Console.WriteLine(receivedPacket.PacketType);
            Console.WriteLine(receivedPacket.Buffer);
            Console.WriteLine("Rcv packet end!");

            if (receivedPacket.PacketType == 0x7)
            {
                Console.WriteLine("0x7 packet!");
                Room r7 = MessagePackSerializer.Deserialize<Room>(receivedPacket.Buffer);
                foreach (var key in r7.Players.Values)
                {
                    Console.WriteLine(key.LoginToken);
                }
                Console.WriteLine(r7.Players.Count);
                Console.WriteLine("End of 0x7 packet");
            }
            
            BlockingCollection<DefaultPacket> q = new BlockingCollection<DefaultPacket>();
            q.Add(receivedPacket);
            DefaultPacket t = q.Take();
            Console.WriteLine("am scos: " + t.PacketType);
            
            
            WorldManagerQueue.Enqueue(receivedPacket);
            Console.WriteLine(Globals.MAX_RING_SIZE);

            return;*/
            ENet.Library.Initialize();
            RingBuffer<DefaultPacket> WorldManagerQueue = new RingBuffer<DefaultPacket>(Globals.MAX_RING_SIZE);
            RingBuffer<DefaultPacket> AuthQueue = new RingBuffer<DefaultPacket>(Globals.MAX_RING_SIZE);
            RingBuffer<DefaultPacket> SendingQueue = new RingBuffer<DefaultPacket>(Globals.MAX_RING_SIZE);
            
            ClientChannelCommunicator clCommunicator = new ClientChannelCommunicator(ref WorldManagerQueue);
            GameServerChannelCommunicator gsCommunicator = new GameServerChannelCommunicator();
            WorldServerManager wsManager = new WorldServerManager(ref WorldManagerQueue, ref AuthQueue, ref SendingQueue);
            AuthManager authManager = new AuthManager(ref AuthQueue, ref SendingQueue);

            /* Common Blocking Queue for incoming login attempts */
            
            /* Thread 1: Networking with the clients */
            //Thread clThread = new Thread(() => clCommunicator.Launch());
            
            /* Thread 2: Networking with the Game Server */
            Thread gsThread = new Thread(() => gsCommunicator.Launch());
            
            /* Thread 3: World Server Manager */
            Thread worldThread = new Thread(() => wsManager.Launch());
            
            /* Thread 4: Auth + Database Interrogator Thread */
            Thread authThread = new Thread(() => authManager.Launch());
            
            /* Start threads */
          //  clThread.Start();
            gsThread.Start();
            worldThread.Start();
            authThread.Start();
            
            DefaultPacket dp = new DefaultPacket(
                PacketType.LOGIN_PKT,
                new LoginPacket(
                    "goguign",
                    "gogu",
                    "salut"
                    ).AsByteArray()
                );
            
            WorldManagerQueue.Enqueue(dp);
            
            /* Join threads */
           // clThread.Join();
            gsThread.Join();
            worldThread.Join();
            authThread.Join();
            
            ENet.Library.Deinitialize();
        }
    }
}