using Account;
using Network.Packets;
using Network.Packets.Instance.Network.Packets.RoomPackets;
using Network.Packets.RoomPackets;
using UI;
using UnityEngine;
using utils;

namespace Events
{
    public class StartGameEvent : MonoBehaviour
    {
        public void StartGameSignal()
        {
            GameLogic gameLogic = GameLogic.Instance;
            MyAccount account = MyAccount.Instance;

            var currentRoom = account.GetCurrentRoom().Value;

            DefaultPacket packet;
            Debug.Log("START room " + currentRoom + " by " + account.GetIGN());
        
            packet = new DefaultPacket(
                PacketType.START_GAME_PKT,
                new StartGamePacket(
                        currentRoom,
                        account.GetIGN()
                        )
                    .AsByteArray()
            );
        
            gameLogic.Send(ref packet);
        }
    }
}