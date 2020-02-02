using System;
using DisruptorUnity3d;
using Network.Packets;
using UnityEngine;

public class GameLogic : Singleton<GameLogic>
{
    private RingBuffer<DefaultPacket> _ringQueue;
    
    protected GameLogic () {}
    private void Awake()
    {
        Debug.Log("[Awake] GameLogic");
    }

    public void SetRingQueue(ref RingBuffer<DefaultPacket> ringQueue)
    {
        _ringQueue = ringQueue;
    }

    public void Launch()
    {
        
    }
}
