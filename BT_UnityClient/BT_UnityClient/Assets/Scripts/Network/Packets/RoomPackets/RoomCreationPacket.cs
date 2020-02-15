using Account;
using Interfaces;
using MessagePack;

namespace Network.Packets.RoomPackets
{
    [MessagePackObject]
    public class RoomCreationPacket : ISerialize
    {
        [Key(0)] public byte Map;
        [Key(1)] public byte GameMode;
        [Key(2)] public byte MaxPlayers;
        [Key(3)] public string OwnerIgn;
        [Key(4)] public string Token;

        [SerializationConstructor]
        public RoomCreationPacket(byte gameMode, byte map, byte maxPlayers)
        {
            Map = map;
            GameMode = gameMode;
            MaxPlayers = maxPlayers;
            OwnerIgn = MyAccount.Instance.GetIGN();
            Token = MyAccount.Instance.GetToken();
        }
        
        public byte[] AsByteArray()
        {
            return MessagePackSerializer.Serialize(this);
        }

        public RoomCreationPacket Deserialize(byte[] bytes)
        {
            return MessagePackSerializer.Deserialize<RoomCreationPacket>(bytes);
        }
    }
}