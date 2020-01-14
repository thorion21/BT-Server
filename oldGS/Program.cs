using System;
using System.Threading;
using BT_Server.DataTypes;
using BT_Server.Factories;
using BT_Server.utils;
using BT_Server.libs.Serialization;

namespace BT_Server
{
    internal class MainExecute
    {
        public static void Main(string[] args)
        {
            var obj = PacketFactory.Build(PacketType.MOVEMENT_PKT, 1, "salut", true);
            
            
            /*ENetBase enet = new ENetBase();
            Thread ENetServerThread = new Thread(() => enet.Launch());
            ENetServerThread.Start();*/

            
            // Create a new bit buffer with 1024 chunks, the buffer can grow automatically if required
            BitBuffer data = new BitBuffer(1024);

            // Fill bit buffer and serialize data to a byte array
            uint peer = 12;
            string name = "smecheru";
            string username = "usr";
            string password = "pw";
            
            byte[] buffer = new byte[64];
            
            data.AddByte(0xA)
                .AddUInt(peer)
                .AddString(name)
                .AddString(username)
                .AddString(password)
                .ToArray(buffer);

            // Get a length of actual data in bit buffer for sending through the network
            Console.WriteLine("Data length: " + data.Length);
            Console.WriteLine("Data: " + buffer.Length);

            // Reset bit buffer for further reusing
            data.Clear();

            var typeVal = (LoginType) DataTypeFactory.Deserialize(buffer);
            Console.WriteLine(typeVal.Name + " " + typeVal.Password + " " + typeVal.Username);

            /*
            BitBuffer data1 = new BitBuffer(64);
            

            // Deserialize data from a byte array
            data1.FromArray(buffer, 64);

            // Unload bit buffer in the same order
            uint peer1 = data1.ReadUInt();
            string name1 = data1.ReadString();
            bool accelerated1 = data1.ReadBool();
            ushort speed1 = data1.ReadUShort();
            byte type = data1.ReadByte();
            
            Console.WriteLine(peer1 + " " + name1 + " " + accelerated1 + " " + speed1 + " " + type);
            Console.WriteLine("Bit buffer is empty: " + data1.IsFinished);
            // Check if bit buffer is fully unloaded
            
            data1.Clear();
            Console.WriteLine("Bit buffer is empty: " + data1.IsFinished);*/
        }
    }
}