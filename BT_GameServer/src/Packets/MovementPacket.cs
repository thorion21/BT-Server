using System;
using BT_GameServer.Interfaces;
using BT_GameServer.libs.Serialization;
using BT_GameServer.utils;

namespace BT_GameServer.Packets
{
    public class MovementPacket : DefaultPacket, IPacket
    {
        public MovementPacket(params object[] args) : base()
        {
            Console.WriteLine("Movement Packet constructor called.");
            data.AddByte(PacketType.MOVEMENT_PKT)
                .AddInt((int) args[0])
                .ToArray(buffer);
            
            data.Clear();
        }
    }
}