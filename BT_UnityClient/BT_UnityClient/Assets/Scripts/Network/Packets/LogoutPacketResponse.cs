using Interfaces;
using MessagePack;

namespace Network.Packets
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
        
        public static LogoutPacketResponse Deserialize(byte[] bytes)
        {
            return MessagePackSerializer.Deserialize<LogoutPacketResponse>(bytes);
        }
    }
}