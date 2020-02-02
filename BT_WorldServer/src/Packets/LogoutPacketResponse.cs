using BT_WorldServer.Interfaces;
using MessagePack;

namespace BT_WorldServer.Packets
{
    [MessagePackObject]
    public class LogoutPacketResponse : ISerialize
    {
        [Key(0)] public byte Status;

        [SerializationConstructor]
        public LogoutPacketResponse(byte status)
        {
            Status = status;
        }

        public byte[] AsByteArray()
        {
            return MessagePackSerializer.Serialize(this);
        }
    }
}