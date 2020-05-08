using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;
using utils;

namespace Entities.Room
{
    public class RoomManager : Singleton<RoomManager>
    {
        private Dictionary<ushort, Room> _rooms;
        private UiManager _ui;
        public GameObject grid;

        private void Awake()
        {
            _rooms = new Dictionary<ushort, Room>();
            _ui = UiManager.Instance;
        }

        private void Update()
        {
            /*string temp = "";

            foreach (var room in GetRoomsList())
            {
                temp += room.RoomID + " " + room.Owner.IGN + " " + room.Map + "\n";
            }

            RoomText.GetComponent<Text>().text = temp;*/
        }

        public void AddRoom(Room room)
        {
            _rooms[room.RoomID] = room;
            _ui.Signal(UiEvents.AddRoomInList, room);
        }

        public void RemoveRoom(ushort roomId)
        {
            _rooms.Remove(roomId);
        }

        public Room GetRoom(ushort roomId)
        {
            return _rooms[roomId];
        }

        public void ClearRooms()
        {
            _rooms.Clear();
        }
        
        public Dictionary<ushort, Room>.ValueCollection GetRoomsList()
        {
            return _rooms.Values;
        }
    }
}