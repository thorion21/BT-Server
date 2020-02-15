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
        public GameObject createTab;
        private RingBuffer<byte> _events;

        private void Awake()
        {
            _events = new RingBuffer<byte>(Globals.UI_EVENTS_LIMIT);
            loginMenu.SetActive(true);
            lobbyMenu.SetActive(false);
            createTab.SetActive(false);
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
                        LogoutSuccessfulTransition();
                        break;
                    case UiEvents.LobbyMenuTransition:
                        LoginSuccessfulTransition();
                        break;
                    case UiEvents.CreateRoomTransition:
                        SetCreateRoomTab(true);
                        break;
                    case UiEvents.CloseRoomTransition:
                        SetCreateRoomTab(false);
                        break;
                }
            }
        }

        private void LoginSuccessfulTransition()
        {
            loginMenu.SetActive(false);
            lobbyMenu.SetActive(true);
        }

        private void LogoutSuccessfulTransition()
        {
            loginMenu.SetActive(true);
            lobbyMenu.SetActive(false);
        }

        private void SetCreateRoomTab(bool value)
        {
            createTab.SetActive(value);
        }
    }
}