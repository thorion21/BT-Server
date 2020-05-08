using System;
using System.Collections.Generic;
using Network.Packets;
using Network.Packets.RoomPackets;
using UI;
using UnityEngine;
using UnityEngine.UI;
using utils;

namespace Events
{
    public class CreateRoomEvent : MonoBehaviour
    {
        private GameLogic _gameLogic;
        private UiManager _ui;
        private byte _map, _gameMode, _maxPlayers;

        public Dropdown gameModeChoicesDropDown,
            mapChoicesDropDown,
            maxPlayersChoicesDropDown;

        private List<string> _mapChoices, _gameModeChoices, _maxPlayersChoices; 

        void Awake()
        {
            _gameLogic = GameLogic.Instance;
            _ui = UiManager.Instance;
            
            _gameModeChoices = new List<string> {"TDM", "SD"};
            _mapChoices = new List<string> {"Omnyx", "Sanctuary"};
            _maxPlayersChoices = new List<string>();

            _map = 0;
            _gameMode = 0;
            _maxPlayers = 2;
            
            for (int i = 1; i <= 6; ++i)
                _maxPlayersChoices.Add(Math.Pow(2, i).ToString());
        }

        void Start()
        {
            gameModeChoicesDropDown.AddOptions(_gameModeChoices);
            mapChoicesDropDown.AddOptions(_mapChoices);
            maxPlayersChoicesDropDown.AddOptions(_maxPlayersChoices);
        }

        public void OnSetMapCallback(int index) { _map = (byte) index; }
        public void OnSetGameModeCallback(int index) { _gameMode = (byte) index; }
        public void OnSetMaxPlayersCallback(int index) { _maxPlayers = (byte) Math.Pow(2, index + 1); }

        public void OpenCreateTab()
        {
            _ui.Signal(UiEvents.CreateRoomTransition);
        }

        public void CloseCreateTab()
        {
            gameModeChoicesDropDown.value = 0;
            mapChoicesDropDown.value = 0;
            maxPlayersChoicesDropDown.value = 0;
            
            _ui.Signal(UiEvents.CloseRoomTransition);
        }

        public void CreateRoomSignal()
        {
            DefaultPacket packet;
            Debug.Log("Create room with gamemode " + _gameMode + ", map " + _map + " and max players " + _maxPlayers);
        
            packet = new DefaultPacket(
                PacketType.CREATE_ROOM_PKT,
                new RoomCreationPacket(
                        _gameMode,
                        _map,
                        _maxPlayers
                        )
                    .AsByteArray()
            );
        
            _gameLogic.Send(ref packet);
        }
    }
}