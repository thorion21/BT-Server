using BT_Server.DataTypes;
using BT_Server.Interfaces;
using BT_Server.libs.Serialization;
using BT_Server.utils;

namespace BT_Server.Factories
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