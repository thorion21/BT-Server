using Account;
using Interfaces;
using MessagePack;

namespace Network.Packets
{
    [MessagePackObject]
    public class LogoutPacket : ISerialize
    {
        [Key(0)] public string IGN;
        [Key(1)] public string Token;

        [SerializationConstructor]
        public LogoutPacket()
        {
            IGN = MyAccount.Instance.GetIGN();
            Token = MyAccount.Instance.GetToken();
        }
        
        public byte[] AsByteArray()
        {
            return MessagePackSerializer.Serialize(this);
        }
        
        public static LogoutPacket Deserialize(byte[] bytes)
        {
            return MessagePackSerializer.Deserialize<LogoutPacket>(bytes);
        }
    }
}