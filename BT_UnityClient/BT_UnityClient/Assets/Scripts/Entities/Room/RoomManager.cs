using System;
using System.Collections.Generic;

namespace Entities.Room
{
    public class RoomManager : Singleton<RoomManager>
    {
        private Dictionary<ushort, Room> _rooms;

        private void Awake()
        {
            _rooms = new Dictionary<ushort, Room>();
        }

        public void AddRoom(Room room)
        {
            _rooms[room.RoomID] = room;
        }

        public void RemoveRoom(ushort roomId)
        {
            _rooms.Remove(roomId);
        }
        
        public Dictionary<ushort, Room>.ValueCollection GetRoomsList()
        {
            return _rooms.Values;
        }
    }
}