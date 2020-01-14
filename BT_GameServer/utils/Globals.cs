namespace BT_GameServer.utils
{
    public static class Globals
    {
        public const string SERVER_ADDR = "127.0.0.1";
        public const ushort PORT = 9999;
        public const int MAX_CLIENTS = 128;
        public const int CAPACITY = 64;
    }

    public struct PacketType
    {
        public const byte LOGIN_PKT = 0xA;
        public const byte LOGIN_RSP_PKT = 0xAA;
        public const byte LOGOUT_PKT = 0xB;
        public const byte LOGOUT_RSP_PKT = 0xBB;
        public const byte MOVEMENT_PKT = 0xC;
        public const byte FIRE_PKT = 0xD;
        public const byte INPUT_PKT = 0xE;
    }
}