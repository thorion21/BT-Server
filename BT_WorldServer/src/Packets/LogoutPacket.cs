using BT_WorldServer.Interfaces;
using MessagePack;

namespace BT_WorldServer.Packets
{
    [MessagePackObject]
    public class LogoutPacket : ISerialize
    {
        [Key(0)] public string IGN;
        [Key(1)] public string Username;
        [Key(2)] public string Password;

        [SerializationConstructor]
        public LogoutPacket(string ign, string username, string password)
        {
            IGN = ign;
            Username = username;
            Password = password;
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