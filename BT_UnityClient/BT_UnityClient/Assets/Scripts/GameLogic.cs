using System;
using System.Collections.Concurrent;
using System.Threading;
using Account;
using DisruptorUnity3d;
using ENet;
using Entities;
using Entities.Lobby;
using Entities.PlayersLobby;
using Entities.Room;
using Network.Packets;
using Network.Packets.Instance;
using Network.Routines;
using UI;
using UnityEngine;
using utils;

public class GameLogic : Singleton<GameLogic>
{
    public BlockingCollection<DefaultPacket> _ringQueue;
    public BlockingCollection<DefaultPacket> _sendingQueue;
    public BlockingCollection<DefaultPacket> _gsSendingQueue;
    private Thread _gameLogicThread;
    private MyAccount _account;
    private UiManager _ui;
    private Lobby _lobby;
    private RoomManager _roomManager;
    private GameServerComm _gsComm;
    public GameInstance GameInstance;
    private GameInstance _gInstance;
    
    protected GameLogic () {}

    private void Awake()
    {
        Debug.Log("[Awake] GameLogic");

        _account = MyAccount.Instance;
        _ui = UiManager.Instance;
        _lobby = Lobby.Instance;
        _roomManager = RoomManager.Instance;
        _gsComm = GameServerComm.Instance;
        _gInstance = GameInstance.GetComponent<GameInstance>();

    }

    public void SetDataQueuesAndLaunch(
        ref BlockingCollection<DefaultPacket> ringQueue,
        ref BlockingCollection<DefaultPacket> sendingQueue,
        ref BlockingCollection<DefaultPacket> gsSendingQueue
        )
    {
        _ringQueue = ringQueue;
        _sendingQueue = sendingQueue;
        _gsSendingQueue = gsSendingQueue;
        
        _gameLogicThread = new Thread(Launch);
        _gameLogicThread.IsBackground = true;
        
        _gameLogicThread.Start();
    }

    public void Launch()
    {
        while (true)
        {
            var packet = _ringQueue.Take();
            
            switch (packet.PacketType)
            {
                case PacketType.LOGIN_RSP_PKT:   //  170
                    Debug.Log("PacketType.LOGIN_RSP_PKT");
                    if (LoginRoutine.Check(ref _account, ref packet))
                        _ui.Signal(UiEvents.LobbyMenuTransition);
                    break;
                case PacketType.LOGOUT_RSP_PKT:  //  187
                    Debug.Log("PacketType.LOGOUT_RSP_PKT");
                    if (LogoutRoutine.Execute(ref _account, ref packet))
                        _ui.Signal(UiEvents.LoginMenuTransition);
                    break;
                case PacketType.LOBBY_UPDATE_PKT:  // 13
                    Debug.Log("PacketType.LOBBY_UPDATE_PKT");
                    LobbyResponseHandler.Handle(ref _lobby, ref packet);
                    break;
                case PacketType.ROOM_UPDATE_PKT:  // 14
                    Debug.Log("PacketType.ROOM_UPDATE_PKT");
                    RoomResponseHandler.Handle(ref _roomManager, ref packet);
                    break;
                case PacketType.ROOM_JOIN_RSP_PKT:  // 173
                    Debug.Log("PacketType.ROOM_JOIN_RSP_PKT");
                    if (RoomJoinHandler.Handle(ref _roomManager, ref packet))
                        _ui.Signal(UiEvents.OpenRoomJoinTransition);
                    break;
                case PacketType.ROOM_EXIT_RSP_PKT: // 174
                    Debug.Log("PacketType.ROOM_EXIT_RSP_PKT");
                    if (RoomExitHandler.Handle(ref _roomManager, ref packet))
                        _ui.Signal(UiEvents.CloseRoomJoinTransition);
                    break;
                case PacketType.LAUNCH_INSTANCE_PKT: // 238
                    /* Change scene start game */
                    Debug.Log("PacketType.LAUNCH_INSTANCE_PKT");
                    LaunchInstanceHandler.Handle(ref packet);
                    break;
                case PacketType.GS_INSTANCE_READY: //254
                    Debug.Log("PacketType.GS_INSTANCE_READY");
                    InstanceReadyHandler.Handle(ref packet);
                    break;
                case PacketType.GS_START_GAME: // 252
                    Debug.Log("PacketType.GS_START_GAME");
                    LaunchGameHandler.Handle(ref packet, ref _gInstance);
                    break;
                case PacketType.CORRECTIONS_PKT:
                    //Debug.Log("PacketType.CORRECTIONS_PKT");
                    GSCorrectionsHandler.Handle(ref packet, ref _gInstance);
                    break;
                case PacketType.WORLD_STATE_PKT:
                    //Debug.Log("PacketType.WORLD_STATE_PKT");
                    GSWorldStateHandler.Handle(ref packet, ref _gInstance);
                    break;
                case PacketType.UPDATE_HEALTH_PKT:
                    //Debug.Log("PacketType.WORLD_STATE_PKT");
                    GSUpdateHealth.Handle(ref packet, ref _gInstance);
                    break;
            }
        }
    }

    public void Send(ref DefaultPacket packet)
    {
        _sendingQueue.Add(packet);
    }

    public void SendGS(ref DefaultPacket packet)
    {
        _gsSendingQueue.Add(packet);
    }
    
}
