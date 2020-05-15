namespace utils
{
    public class Globals
    {
        public const string WORLD_SERVER_ADDR = "34.65.75.179";
        public const ushort WORLD_SERVER_PORT = 8000;
        public const int CAPACITY = 64;
        public const int MAX_RING_SIZE = (1 << 16) - 1;
        public const int TICK_RATE = 64;
        public const double TICK_TIME = 1000.0f / TICK_RATE;
        public const byte DEFAULT_CHANNEL = 0;
        public const short UI_EVENTS_LIMIT = 16;
    }

    public struct PacketType
    {
        public const byte LOGIN_PKT = 0xA;
        public const byte LOGIN_RSP_PKT = 0xAA;
        public const byte LOGOUT_PKT = 0xB;
        public const byte LOGOUT_RSP_PKT = 0xBB;
        public const byte CREATE_ROOM_PKT = 0xC;
        public const byte LOBBY_UPDATE_PKT = 0xD;
        public const byte ROOM_UPDATE_PKT = 0xE;
        public const byte ROOM_EXIT_PKT = 0xF;
        public const byte ROOM_JOIN_PKT = 0xAB;
        public const byte ROOM_JOIN_RSP_PKT = 0xAD;
        public const byte ROOM_EXIT_RSP_PKT = 0xAE;
        public const byte START_GAME_PKT = 0xAF;
        public const byte LAUNCH_INSTANCE_PKT = 0xEE;
    }

    public struct LoginStatus
    {
        public const byte LOGIN_SUCCESS = 0x0;
        public const byte LOGIN_FAILED = 0x1;
        public const byte LOGOUT_ACCEPTED = 0x2;
    }

    public struct GameModes
    {
        public const byte GM_DEATHMATCH = 0x0;
        public const byte GM_SND = 0x1;
    }

    public struct Map
    {
        public const byte MAP_DEFAULT = 0x0;
    }

    public struct RoomStatus
    {
        public const byte ROOM_STATUS_WAITING = 0x0;
        public const byte ROOM_STATUS_PLAYING = 0x1;
        public const byte ROOM_START = 0x2;
        public const byte ROOM_JOIN = 0x3;
        public const byte ROOM_CREATE = 0x4;
    }
    
    public struct UiEvents
    {
        public const byte LobbyMenuTransition = 0x0;
        public const byte LoginMenuTransition = 0x1;
        public const byte CreateRoomTransition = 0x2;
        public const byte CloseRoomTransition = 0x3;
        public const byte AddRoomInList = 0x4;
        public const byte OpenRoomJoinTransition = 0x5;
        public const byte CloseRoomJoinTransition = 0x6;
        public const byte EmptyRoomSlots = 0x7;
    }
}