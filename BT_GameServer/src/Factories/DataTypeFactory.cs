using BT_GameServer.DataTypes;
using BT_GameServer.Interfaces;
using BT_GameServer.libs.Serialization;
using BT_GameServer.utils;

namespace BT_GameServer.Factories
{
    public class DataTypeFactory
    {
        public static IData Deserialize(byte[] buffer)
        {
            BitBuffer data = new BitBuffer(Globals.CAPACITY);
            data.FromArray(buffer, Globals.CAPACITY);

            byte packetType = data.ReadByte();

            switch (packetType)
            {
                case PacketType.INPUT_PKT:
                    return new InputType(ref data);
                default:
                    return null;
            }
        }
    }
}