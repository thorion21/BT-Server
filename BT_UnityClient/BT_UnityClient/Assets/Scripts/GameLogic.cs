using System;
using System.Threading;
using Account;
using DisruptorUnity3d;
using ENet;
using Entities;
using Entities.Lobby;
using Entities.PlayersLobby;
using Entities.Room;
using Network.Packets;
using Network.Routines;
using UI;
using UnityEngine;
using utils;

public class GameLogic : Singleton<GameLogic>
{
    private RingBuffer<DefaultPacket> _ringQueue;
    private RingBuffer<DefaultPacket> _sendingQueue;
    private Thread _gameLogicThread;
    private MyAccount _account;
    private UiManager _ui;
    private Lobby _lobby;
    private RoomManager _roomManager;
    
    protected GameLogic () {}

    private void Awake()
    {
        Debug.Log("[Awake] GameLogic");

        _account = MyAccount.Instance;
        _ui = UiManager.Instance;
        _lobby = Lobby.Instance;
        _roomManager = RoomManager.Instance;
    }

    public void SetDataQueuesAndLaunch(
        ref RingBuffer<DefaultPacket> ringQueue,
        ref RingBuffer<DefaultPacket> sendingQueue
        )
    {
        _ringQueue = ringQueue;
        _sendingQueue = sendingQueue;
        
        _gameLogicThread = new Thread(Launch);
        _gameLogicThread.IsBackground = true;
        
        _gameLogicThread.Start();
    }

    public void Launch()
    {
        while (true)
        {
            var packet = _ringQueue.Dequeue();
            
            switch (packet.PacketType)
            {
                case PacketType.LOGIN_RSP_PKT:
                    if (LoginRoutine.Check(ref _account, ref packet))
                        _ui.Signal(UiEvents.LobbyMenuTransition);
                    break;
                case PacketType.LOGOUT_RSP_PKT:
                    if (LogoutRoutine.Execute(ref _account, ref packet))
                        _ui.Signal(UiEvents.LoginMenuTransition);
                    break;
                case PacketType.LOBBY_UPDATE_PKT:
                    LobbyResponseHandler.Handle(ref _lobby, ref packet);
                    break;
                case PacketType.ROOM_UPDATE_PKT:
                    RoomResponseHandler.Handle(ref _roomManager, ref packet);
                    break;
            }
        }
    }

    public void Send(ref DefaultPacket packet)
    {
        _sendingQueue.Enqueue(packet);
    }
}
