using System;
using Account;
using Entities.Room;
using Events;
using UnityEditor.Experimental.SceneManagement;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UiHelperFunctions : MonoBehaviour
    {
        public static void InsertRoomSlot(ref GameObject grid, Room room)
        {
            GameObject button = (GameObject) Instantiate(Resources.Load("ButtonPrefab"));
            button.name = "Room " + room.RoomID;
            button.GetComponent<Button>().onClick.AddListener(delegate { JoinRoomDelegate(room.RoomID); });
            button.transform.SetParent(grid.transform);
            button.transform.GetChild(0).GetComponent<Text>().text = BuildFormat(ref room);
        }

        public static void RemoveAllRoomSlots(ref GameObject grid)
        {
            foreach (Transform child in grid.transform) {
                Destroy(child.gameObject);
            }
        }

        private static void JoinRoomDelegate(ushort id)
        {
            JoinRoomEvent.JoinRoomSignal(id);
        }

        private static string BuildFormat(ref Room room)
        {
            return room.RoomID + "\t" + room.Owner.IGN + "\t" + room.Map + "\t" + room.Players.Count + "/" +
                   room.MaxPlayers + "\t" + room.Status;
        }
    }
}