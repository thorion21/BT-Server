using Network.Packets;
using UnityEngine;
using UnityEngine.UI;
using utils;

namespace Authentication
{
    public class LoginEvent : MonoBehaviour
    {
        public GameObject UsernameObject;
        public GameObject PasswordObject;
        private GameLogic _gameLogic;

        private string _username, _password;

        void Awake()
        {
            _gameLogic = GameLogic.Instance;
        }
    
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (UsernameObject.GetComponent<InputField>().isFocused)
                {
                    PasswordObject.GetComponent<InputField>().Select();
                }
            }
        }

        public void SendLoginSignal()
        {
            DefaultPacket packet;
            _username = UsernameObject.GetComponent<InputField>().text;
            _password = PasswordObject.GetComponent<InputField>().text;
            Debug.Log(_username + " " + _password);
        
            packet = new DefaultPacket(
                PacketType.LOGIN_PKT,
                new LoginPacket(
                    "goguIGN",
                    _username,
                    _password
                ).AsByteArray()
            );
        
            _gameLogic.Send(ref packet);
        }
    }
}
