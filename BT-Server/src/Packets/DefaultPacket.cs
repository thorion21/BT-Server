using System;
using BT_Server.libs.Serialization;
using BT_Server.utils;

namespace BT_Server.Packets
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