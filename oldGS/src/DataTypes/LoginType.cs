using BT_Server.Interfaces;
using BT_Server.libs.Serialization;
using ENet;

namespace BT_Server.DataTypes
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