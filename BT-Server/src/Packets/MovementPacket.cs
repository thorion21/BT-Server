using System;
using BT_Server.Interfaces;
using BT_Server.libs.Serialization;
using BT_Server.utils;

namespace BT_Server.Packets
{
    public class MovementPacket : DefaultPacket, IPacket
    {
        public MovementPacket(params object[] args) : base()
        {
            Console.WriteLine("Movement Packet constructor called.");
            data.AddByte(PacketType.MOVEMENT_PKT)
                .AddInt((int) args[0])
                .ToArray(buffer);
        }
    }
}