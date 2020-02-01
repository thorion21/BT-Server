﻿namespace BT_WorldServer.utils
{
    public static class Globals
    {
        public const string WORLD_SERVER_ADDR = "127.0.0.1";
        public const string INTERNAL_GAME_SERVER_ADDR = "127.0.0.1";
        public const ushort WORLD_SERVER_PORT = 8000;
        public const ushort INTERNAL_GAME_SERVER_PORT = 13000;
        public const int MAX_CLIENTS = 128;
        public const int CAPACITY = 64;
    }

    public struct PacketType
    {
        public const byte LOGIN_PKT = 0xA;
        public const byte LOGIN_RSP_PKT = 0xAA;
        public const byte LOGOUT_PKT = 0xB;
        public const byte LOGOUT_RSP_PKT = 0xBB;
        public const byte ROOM_PKT = 0xC;
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
    }
}