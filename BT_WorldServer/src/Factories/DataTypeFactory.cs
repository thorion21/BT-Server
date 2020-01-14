using BT_WorldServer.DataTypes;
using BT_WorldServer.Interfaces;
using BT_WorldServer.libs.Serialization;
using BT_WorldServer.utils;

namespace BT_WorldServer.Factories
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
                case PacketType.LOGIN_PKT:
                    return new LoginType(ref data);
                case PacketType.LOGOUT_PKT:
                    return new LogoutType(ref data);
                default:
                    return null;
            }
        }
    }
}