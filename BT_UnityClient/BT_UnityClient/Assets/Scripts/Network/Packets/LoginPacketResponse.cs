using Interfaces;
using MessagePack;

namespace Network.Packets
{
    [MessagePackObject]
    public class LoginPacketResponse : ISerialize
    {
        [Key(0)] public byte Status;
        [Key(1)] public string IGN;
        [Key(2)] public string Token;

        [SerializationConstructor]
        public LoginPacketResponse(byte status, string name="", string token="")
        {
            Status = status;
            IGN = name;
            Token = token;
        }

        public byte[] AsByteArray()
        {
            return MessagePackSerializer.Serialize(this);
        }
        
        public static LoginPacketResponse Deserialize(byte[] bytes)
        {
            return MessagePackSerializer.Deserialize<LoginPacketResponse>(bytes);
        }
    }
}