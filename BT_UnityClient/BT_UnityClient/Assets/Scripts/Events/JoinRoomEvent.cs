using Account;
using Network.Packets;
using Network.Packets.RoomPackets;
using UI;
using UnityEngine;
using utils;

namespace Events
{
    public class JoinRoomEvent : MonoBehaviour
    {
        public static void JoinRoomSignal(ushort id)
        {
            GameLogic gameLogic = GameLogic.Instance;
            MyAccount account = MyAccount.Instance;
                    
            DefaultPacket packet;
            Debug.Log("Join room " + id + " by " + account.GetIGN());
        
            packet = new DefaultPacket(
                PacketType.ROOM_JOIN_PKT,
                new RoomJoinPacket(
                        id,
                        account.GetIGN()
                    )
                    .AsByteArray()
            );
        
            gameLogic.Send(ref packet);
        }
    }
}