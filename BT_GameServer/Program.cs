/*
 * Has a Game Tick of 60Hz
 * Purpose:
 *     1. Communicates with the World Server to get the room data and returns a instance of the game
 *     2. Tracks every room and has workers to interpret input from each player
 *     3. Every tick it processes data from every room
 *     4?. Optimize performance by performing chunk-dependent actions
 *     5. Simulate physics on the GPU using CUDA to increase performance
 *     6. When the game ends it sends back the data to the World Server
 */

using System;
using System.Threading;
using BT_GameServer.utils;

namespace BT_GameServer
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            ENet.Library.Initialize();

            ClientChannelCommunicator clCommunicator = new ClientChannelCommunicator();
            WorldServerChannelCommunicator wsCommunicator = new WorldServerChannelCommunicator();

            /* Thread 1: Networking with the clients */
            Thread clThread = new Thread(() => clCommunicator.Launch());

            /* Thread 2: Networking with the World Server */
            Thread wsThread = new Thread(() => wsCommunicator.Launch());

            /* Game Server creation */
            
            /* Start threads */
            clThread.Start();
            wsThread.Start();
            
            /* Join threads */
            clThread.Join();
            wsThread.Join();
            
            ENet.Library.Deinitialize();
        }
    }
}