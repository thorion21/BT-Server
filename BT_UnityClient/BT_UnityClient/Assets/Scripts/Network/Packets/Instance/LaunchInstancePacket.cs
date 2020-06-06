using Interfaces;
using MessagePack;
namespace Network.Packets.Instance
{
    [MessagePackObject]
    public class LaunchInstancePacket : ISerialize
    {
        [Key(0)] public string GameServerIP;
        [Key(1)] public ushort GameServerPORT;
        [Key(2)] public int InstanceID;

        public LaunchInstancePacket(string svIP, ushort svPORT, int instance_id)
        {
            GameServerIP = svIP;
            GameServerPORT = svPORT;
            InstanceID = instance_id;
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