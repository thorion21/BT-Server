using BT_WorldServer.Interfaces;
using BT_WorldServer.libs.Serialization;

namespace BT_WorldServer.DataTypes
{
    public class LogoutType : IData
    {
        public uint Peer;

        public LogoutType(ref BitBuffer data)
        {
            Peer = data.ReadUInt();
            
            data.Clear();
        }
    }
}