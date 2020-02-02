using System.Diagnostics;
using BT_WorldServer.libs.Disruptor;
using BT_WorldServer.Packets;
using BT_WorldServer.utils;
using BT_WorldServer.WorldServer.Auth;

namespace BT_WorldServer
{
    public class AuthManager
    {
        private RingBuffer<DefaultPacket> _incomingAuthentications;
        private RingBuffer<DefaultPacket> _sendingQueue;
        private Authenticator _authenticator;
        
        public AuthManager(ref RingBuffer<DefaultPacket> auths, ref RingBuffer<DefaultPacket> sending)
        {
            _incomingAuthentications = auths;
            _sendingQueue = sending;
            _authenticator = new Authenticator(this);
        }

        public void Launch()
        {
            /*
             * Listens to incoming authentications, decides if data is correct
             * and returns multiple packets -> CON_ACC, Inventory Data, and adds
             * player in world server thread of online players
             */
            while (true)
            {
                DefaultPacket rPacket = _incomingAuthentications.Dequeue();
                switch (rPacket.PacketType)
                {
                    case PacketType.LOGIN_PKT: _authenticator.Login(ref rPacket); break;
                    case PacketType.LOGOUT_PKT: _authenticator.Logout(ref rPacket); break;
                }
            }
        }

        public void AddPacketToSending(DefaultPacket packet)
        {
            _sendingQueue.Enqueue(packet);
        }
        
    }
}