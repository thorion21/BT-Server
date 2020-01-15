namespace BT_GameServer.utils
{
    public static class Globals
    {
        public const string GAME_SERVER_ADDR = "127.0.0.1";
        public const string INTERNAL_WORLD_SERVER_ADDR = "127.0.0.1";
        public const ushort GAME_SERVER_PORT = 10512;
        public const ushort INTERNAL_WORLD_SERVER_PORT = 13000;
        public const int MAX_CLIENTS = 128;
        public const int CAPACITY = 64;
    }

    public struct PacketType
    {
        public const byte MOVEMENT_PKT = 0xC;
        public const byte FIRE_PKT = 0xD;
        public const byte INPUT_PKT = 0xE;
    }
}