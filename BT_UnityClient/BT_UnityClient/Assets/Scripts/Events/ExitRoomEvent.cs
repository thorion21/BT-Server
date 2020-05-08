using Account;
using Network.Packets;
using Network.Packets.RoomPackets;
using UI;
using UnityEngine;
using utils;

namespace Events
{
    public class ExitRoomEvent : MonoBehaviour
    {
        public void ExitRoomSignal()
        {
            GameLogic gameLogic = GameLogic.Instance;
            MyAccount account = MyAccount.Instance;

            var currentRoom = account.GetCurrentRoom().Value;

            DefaultPacket packet;
            Debug.Log("Exit room " + currentRoom + " by " + account.GetIGN());
        
            packet = new DefaultPacket(
                PacketType.ROOM_EXIT_PKT,
                new RoomExitPacket(
                    account.GetIGN(),
                    currentRoom
                    )
                    .AsByteArray()
            );
        
            gameLogic.Send(ref packet);
        }
    }
}