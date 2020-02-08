using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Entities.Lobby
{
    public class Lobby : Singleton<Lobby>
    {
        private Dictionary<string, LobbyPlayer> _players;
        public GameObject LobbyText;

        public Lobby()
        {
            _players = new Dictionary<string, LobbyPlayer>();
        }

        private void Update()
        {
            string temp = "";
            foreach (var player in GetLobbyPlayerList())
            {
                temp += player.IGN + " ";
            }
            
            LobbyText.GetComponent<Text>().text = temp;
        }

        public void JoinLobby(LobbyPlayer player)
        {
            _players[player.IGN] = player;
        }

        public void RemoveFromLobby(LobbyPlayer player)
        {
            _players.Remove(player.IGN);
        }

        public Dictionary<string, LobbyPlayer>.ValueCollection GetLobbyPlayerList()
        {
            return _players.Values;
        }
    }
}