using System;
using BT_WorldServer.libs.Serialization;
using BT_WorldServer.utils;

namespace BT_WorldServer.Packets
{
    public class DefaultPacket
    {
        protected BitBuffer data;
        protected byte[] buffer;

        public DefaultPacket()
        {
            Console.WriteLine("Default Packet constructor called.");
            data = new BitBuffer(Globals.CAPACITY);
            buffer = new byte[Globals.CAPACITY];
        }

        public byte[] GetByteBuffer()
        {
            return buffer;
        }
    }
}