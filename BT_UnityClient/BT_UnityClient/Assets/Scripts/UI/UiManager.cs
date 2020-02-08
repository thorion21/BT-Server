using System;
using DisruptorUnity3d;
using UnityEngine;
using utils;

namespace UI
{
    public class UiManager : Singleton<UiManager>
    {
        public GameObject loginMenu;
        public GameObject lobbyMenu;
        private RingBuffer<byte> _events;

        private void Awake()
        {
            _events = new RingBuffer<byte>(Globals.UI_EVENTS_LIMIT);
            loginMenu.SetActive(true);
            lobbyMenu.SetActive(false);
        }

        public void Signal(byte @event)
        {
            _events.Enqueue(@event);
        }

        private void Update()
        {
            while(_events.TryDequeue(out var @event))
            {
                switch (@event)
                {
                    case UiEvents.LoginMenuTransition:
                        break;
                    case UiEvents.LobbyMenuTransition:
                        LoginSuccessfulTransition();
                        break;
                }
            }
        }

        private void LoginSuccessfulTransition()
        {
            loginMenu.SetActive(false);
            lobbyMenu.SetActive(true);
        }
        
        private void LogoutTransition()
        {
            loginMenu.SetActive(true);
            lobbyMenu.SetActive(false);
        }
    }
}