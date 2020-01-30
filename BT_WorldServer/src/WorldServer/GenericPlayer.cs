namespace BT_WorldServer.WorldServer
{
    public class GenericPlayer
    {
        public string LoginToken;
        public string IGN;

        public GenericPlayer(string name, string token)
        {
            IGN = name;
            LoginToken = token;
        }
    }
}