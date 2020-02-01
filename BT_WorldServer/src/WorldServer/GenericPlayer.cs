using BT_WorldServer.libs.Serialization;
using BT_WorldServer.utils;
using MessagePack;

namespace BT_WorldServer.WorldServer
{
    [MessagePackObject]
    public class GenericPlayer
    {
        [Key(0)]
        public string LoginToken;
        [Key(1)]
        public string IGN;

        public GenericPlayer(string name, string token)
        {
            IGN = name;
            LoginToken = token;
        }
    }
}