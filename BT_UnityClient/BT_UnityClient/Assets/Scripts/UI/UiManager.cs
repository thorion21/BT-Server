using System;
using DisruptorUnity3d;
using Entities.Room;
using Events;
using UnityEngine;
using UnityEngine.UI;
using utils;

namespace UI
{
    public class UiManager : Singleton<UiManager>
    {
        public GameObject loginMenu;
        public GameObject lobbyMenu;
        public GameObject createTab;
        public GameObject roomTab;
        public GameObject grid;
        public GameObject createBtn, leaveBtn;
        private RingBuffer<Tuple<byte, object>> _events;

        private void Awake()
        {
            _events = new RingBuffer<Tuple<byte, object>>(Globals.UI_EVENTS_LIMIT);
            loginMenu.SetActive(true);
            lobbyMenu.SetActive(false);
            createTab.SetActive(false);
            roomTab.SetActive(false);
        }

        public void Signal(byte @event, object obj = null)
        {
            _events.Enqueue(new Tuple<byte, object>(@event, obj));
        }

        private void Update()
        {
            while(_events.TryDequeue(out var @event))
            {
                switch (@event.Item1)
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
                    case UiEvents.AddRoomInList:
                        InsertRoomSlot(@event.Item2);
                        break;
                    case UiEvents.OpenRoomJoinTransition:
                        SetJoinedRoomTab(true);
                        break;
                    case UiEvents.CloseRoomJoinTransition:
                        SetJoinedRoomTab(false);
                        break;
                    case UiEvents.EmptyRoomSlots:
                        RemoveAllRoomSlots();
                        break;
                    case UiEvents.GameStart:
                        LaunchGame();
                        break;
                }
            }
        }

        private void InsertRoomSlot(object obj)
        {
            UiHelperFunctions.InsertRoomSlot(ref grid, (Room) obj);
        }

        private void RemoveAllRoomSlots()
        {
            UiHelperFunctions.RemoveAllRoomSlots(ref grid);
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
            createTab.SetActive(false);
            roomTab.SetActive(false);
        }

        private void SetCreateRoomTab(bool value)
        {
            createTab.SetActive(value);
        }

        private void SetJoinedRoomTab(bool value)
        {
            roomTab.SetActive(value);
            createTab.SetActive(false);

            if (value)
            {
                createBtn.SetActive(false);
                leaveBtn.SetActive(false);
            }
            else
            {
                createBtn.SetActive(true);
                leaveBtn.SetActive(true);
            }   
        }

        private void LaunchGame()
        {
            loginMenu.SetActive(false);
            lobbyMenu.SetActive(false);
            createTab.SetActive(false);
            roomTab.SetActive(false);
        }
    }
}