using Network.Packets;
using Network.Packets.RoomPackets;
using UI;
using UnityEngine;
using utils;

namespace Entities.Room
{
    public static class RoomResponseHandler
    {
        private static UiManager _ui = UiManager.Instance;
        public static void Handle(ref RoomManager roomManager, ref DefaultPacket packet)
        {
            RoomUpdatePacket response = RoomUpdatePacket.Deserialize(packet.Buffer);

            if (response.Full)
            {
                _ui.Signal(UiEvents.EmptyRoomSlots);
                roomManager.ClearRooms();
            }

            foreach (var room in response.Added)
            {
                roomManager.AddRoom(room);
                Debug.Log("Room " + room.RoomID + " was created or notified");
            }

            foreach (var room in response.Removed)
            {
                roomManager.RemoveRoom(room.RoomID);
                Debug.Log("Room " + room.RoomID + " was removed");
            }
        }
    }
}