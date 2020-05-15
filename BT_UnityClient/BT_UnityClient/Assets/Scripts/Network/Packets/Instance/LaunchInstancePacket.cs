using Interfaces;
using MessagePack;
namespace Network.Packets.Instance
{
    [MessagePackObject]
    public class LaunchInstancePacket : ISerialize
    {
        [Key(0)] public string GameServerIP;
        [Key(1)] public ushort GameServerPORT;

        public LaunchInstancePacket(string svIP, ushort svPORT)
        {
            GameServerIP = svIP;
            GameServerPORT = svPORT;
        }
        
        public byte[] AsByteArray()
        {
            return MessagePackSerializer.Serialize(this);
        }

        public static LaunchInstancePacket Deserialize(byte[] bytes)
        {
            return MessagePackSerializer.Deserialize<LaunchInstancePacket>(bytes);
        }
    }
}