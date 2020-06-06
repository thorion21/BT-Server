using Interfaces;
using MessagePack;
using Network.Packets.Instance;
using utils;

namespace Network.Packets.Game
{
    [MessagePackObject]
    public class InputsPacket : ISerialize
    {
        [Key(0)] public bool left;
        [Key(1)] public bool right;
        [Key(2)] public bool forward;
        [Key(3)] public bool backward;
        [Key(4)] public bool fire;
        [Key(5)] public uint TickNumber;
        [Key(6)] public string ign;
        [Key(7)] public int instanceId;
        [Key(8)] public NetworkVector rotation;
        [Key(9)] public float camY;
        

        [SerializationConstructor]
        public InputsPacket(bool _left, bool _right, bool _forward, bool _backward, bool _fire, uint _tickNumber, string _ign, int _instanceId, NetworkVector _rotation, float _camY)
        {
            left = _left;
            right = _right;
            forward = _forward;
            backward = _backward;
            fire = _fire;
            TickNumber = _tickNumber;
            ign = _ign;
            instanceId = _instanceId;
            rotation = _rotation;
            camY = _camY;
        }
        
        public byte[] AsByteArray()
        {
            return MessagePackSerializer.Serialize(this);
        }

        public static InputsPacket Deserialize(byte[] bytes)
        {
            return MessagePackSerializer.Deserialize<InputsPacket>(bytes);
        }
    }
}