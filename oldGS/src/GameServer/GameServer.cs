namespace BT_Server.GameServer
{
    public class GameServer
    {
        private string GsIP;
        private ushort GsPort;
        
        public GameServer(string _GsIP, ushort _GsPort)
        {
            GsIP = _GsIP;
            GsPort = _GsPort;
        }
        
        
    }
}