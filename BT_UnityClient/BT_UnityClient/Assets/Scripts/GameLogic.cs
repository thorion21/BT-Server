using System;
using System.Threading;
using Account;
using DisruptorUnity3d;
using Entities;
using Entities.Lobby;
using Network.Packets;
using Network.Routines;
using UnityEngine;
using utils;

public class GameLogic : Singleton<GameLogic>
{
    private RingBuffer<DefaultPacket> _ringQueue;
    private RingBuffer<DefaultPacket> _sendingQueue;
    private Thread _gameLogicThread;
    private MyAccount _account;
    private Lobby _lobby;
    
    protected GameLogic () {}

    private void Awake()
    {
        Debug.Log("[Awake] GameLogic");
        
        _gameLogicThread = new Thread(Launch);
        _gameLogicThread.IsBackground = true;

        _account = MyAccount.Instance;
        _lobby = Lobby.Instance;
        
        _gameLogicThread.Start();
    }

    public void SetRingQueue(ref RingBuffer<DefaultPacket> ringQueue)
    {
        _ringQueue = ringQueue;
    }

    public void SetSendingQueue(ref RingBuffer<DefaultPacket> sendingQueue)
    {
        _sendingQueue = sendingQueue;
    }

    public void Launch()
    {
        while (true)
        {
            var packet = _ringQueue.Dequeue();
            
            switch (packet.PacketType)
            {
                case PacketType.LOGIN_RSP_PKT:
                    LoginRoutine.Check(ref _account, ref packet); break;
                case PacketType.LOGOUT_RSP_PKT:
                    LogoutRoutine.Execute(ref _account, ref packet); break;
                case PacketType.LOBBY_UPDATE_PKT:
                    break;
            }
        }
    }

    public void Send(ref DefaultPacket packet)
    {
        _sendingQueue.Enqueue(packet);
    }
}
