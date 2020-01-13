using System;
using System.Threading;
using BT_Server.utils;

namespace BT_Server
{
    internal class MainExecute
    {
        public static void Main(string[] args)
        {
            ENetBase enet = new ENetBase();
            Thread ENetServerThread = new Thread(() => enet.Launch());
            ENetServerThread.Start();
        }
    }
}