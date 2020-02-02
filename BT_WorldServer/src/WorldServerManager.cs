using System;
using BT_WorldServer.libs.Disruptor;
using BT_WorldServer.Packets;
using BT_WorldServer.utils;

namespace BT_WorldServer
{
    public class WorldServerManager
    {
        private RingBuffer<DefaultPacket> _ringBuffer;
        private RingBuffer<DefaultPacket> _authQueue;
        private RingBuffer<DefaultPacket> _sendingQueue;
        
        public WorldServerManager(
            ref RingBuffer<DefaultPacket> rQueue,
            ref RingBuffer<DefaultPacket> authQueue,
            ref RingBuffer<DefaultPacket> sendingQueue
            )
        {
            _ringBuffer = rQueue;
            _authQueue = authQueue;
            _sendingQueue = sendingQueue;
        }
        
        public void Launch()
        {
            /*
             * Processing loop, 
             */

            while (true)
            {
                DefaultPacket rcvPacket = _ringBuffer.Dequeue();

                switch (rcvPacket.PacketType)
                {
                    case PacketType.LOGIN_PKT:
                    case PacketType.LOGOUT_PKT:
                        _authQueue.Enqueue(rcvPacket);
                        break;
                    default:
                        break;
                }
                
            }
        }
    }
}