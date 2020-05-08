using Account;
using Entities.Room;
using UnityEngine;
using UnityEngine.UI;

namespace Events
{
    public class ShowRoomPlayers : MonoBehaviour
    {
        public GameObject textObject;
        private Text txt;
        private MyAccount _account;
        private RoomManager _roomManager;
        private Room _thisRoom;
        
        void Awake()
        {
            _account = MyAccount.Instance;
            _roomManager = RoomManager.Instance;
            txt = textObject.GetComponent<Text>();
        }

        void Update()
        {
            if (_account.GetCurrentRoom().HasValue)
            {
                _thisRoom = _roomManager.GetRoom(_account.GetCurrentRoom().Value);
            
                string temp = "";
            
                foreach (var player in _thisRoom.Players.Values)
                {
                    temp += player.IGN + " ";
                }

                txt.text = temp;
            }
        }
    }
}