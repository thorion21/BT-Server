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

using System;
using System.Threading;
using BT_WorldServer.WorldServer;

namespace BT_WorldServer
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            ENet.Library.Initialize();

            ClientChannelCommunicator clCommunicator = new ClientChannelCommunicator();
            GameServerChannelCommunicator gsCommunicator = new GameServerChannelCommunicator();

            /* Common Blocking Queue for incoming login attempts */
            
            /* Thread 1: Networking with the clients */
            Thread clThread = new Thread(() => clCommunicator.Launch());
            
            /* Thread 2: Networking with the Game Server */
            Thread gsThread = new Thread(() => gsCommunicator.Launch());
            
            /* Thread 3: Database Interrogator */
            
            /* World Server Creation */
            
            /* Start threads */
            clThread.Start();
            gsThread.Start();
            
            /* Join threads */
            clThread.Join();
            gsThread.Join();
            
            ENet.Library.Deinitialize();
        }
    }
}