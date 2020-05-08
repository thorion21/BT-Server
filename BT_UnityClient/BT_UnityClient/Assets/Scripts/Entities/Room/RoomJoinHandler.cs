using Account;
using Network.Packets;
using Network.Packets.RoomPackets;
using UnityEngine;

namespace Entities.Room
{
    public static class RoomJoinHandler
    {
        private static MyAccount _account = MyAccount.Instance;
        public static bool Handle(ref RoomManager roomManager, ref DefaultPacket packet)
        {
            RoomJoinPacketResponse response = RoomJoinPacketResponse.Deserialize(packet.Buffer);

            if (response.Status)
            {
                /* Send player in room with the room id */
                Room roomToJoin = roomManager.GetRoom(response.RoomID);
                _account.SetCurrentRoom(response.RoomID);

                foreach (var player in response.AddedPlayers)
                {
                    roomToJoin.JoinRoom(player);
                }

                foreach (var player in response.RemovedPlayers)
                {
                    roomToJoin.LeaveRoom(player);
                }

                return true;
            }

            return false;
        }
    }
}