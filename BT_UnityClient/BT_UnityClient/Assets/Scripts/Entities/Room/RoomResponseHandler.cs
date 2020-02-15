using Network.Packets;
using Network.Packets.RoomPackets;

namespace Entities.Room
{
    public static class RoomResponseHandler
    {
        public static void Handle(ref RoomManager roomManager, ref DefaultPacket packet)
        {
            RoomUpdatePacket response = RoomUpdatePacket.Deserialize(packet.Buffer);

            foreach (var room in response.NewRooms)
            {
                roomManager.AddRoom(room);
            }

            foreach (var room in response.RemovedRooms)
            {
                roomManager.RemoveRoom(room);
            }
        }
    }
}