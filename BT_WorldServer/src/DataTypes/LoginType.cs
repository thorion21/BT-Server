using BT_WorldServer.Interfaces;
using BT_WorldServer.libs.Serialization;
using ENet;

namespace BT_WorldServer.DataTypes
{
    public class LoginType : IData
    {
        public uint Peer;
        public string Name;
        public string Username;
        public string Password;

        public LoginType(ref BitBuffer data)
        {
            Peer = data.ReadUInt();
            Name = data.ReadString();
            Username = data.ReadString();
            Password = data.ReadString();
            
            data.Clear();
        }
    }
}