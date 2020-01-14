using System;
using BT_GameServer.libs.Serialization;
using BT_GameServer.utils;

namespace BT_GameServer.Packets
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