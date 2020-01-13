using BT_Server.Interfaces;
using BT_Server.libs.Serialization;

namespace BT_Server.DataTypes
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