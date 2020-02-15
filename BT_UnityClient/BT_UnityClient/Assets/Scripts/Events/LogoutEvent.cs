using Account;
using Network.Packets;
using UI;
using UnityEngine;
using UnityEngine.UI;
using utils;

namespace Events
{
    public class LogoutEvent : MonoBehaviour
    {
        private GameLogic _gameLogic;
        
        public void Awake()
        {
            _gameLogic = GameLogic.Instance;
        }

        public void SendLogoutSignal()
        {
            DefaultPacket packet;
            Debug.Log("Send logout signal!");
        
            packet = new DefaultPacket(
                PacketType.LOGOUT_PKT,
                new LogoutPacket().AsByteArray()
            );
            
            _gameLogic.Send(ref packet);
        }
    }
}