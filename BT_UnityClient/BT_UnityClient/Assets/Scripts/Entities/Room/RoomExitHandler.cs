using Account;
using Entities.Lobby;
using Network.Packets;
using Network.Packets.RoomPackets;
using UnityEngine;

namespace Entities.Room
{
    public static class RoomExitHandler
    {
        private static MyAccount _account = MyAccount.Instance;
        public static bool Handle(ref RoomManager roomManager, ref DefaultPacket packet)
        {
            RoomExitPacketResponse response = RoomExitPacketResponse.Deserialize(packet.Buffer);
            Debug.Log("AR TREBUI SA INCHID!");
            /* Send player in room with the room id */
            //Room roomToLeave = roomManager.GetRoom(response.RoomID);
            //roomToLeave.LeaveRoom(new LobbyPlayer(response.IGN));
            _account.SetCurrentRoom(null);

            return true;
        }
    }
}