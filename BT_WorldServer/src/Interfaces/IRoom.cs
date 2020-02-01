using System;
using BT_WorldServer.WorldServer;

namespace BT_WorldServer.Interfaces
{
    public interface IRoom
    {
        bool JoinRoom(GenericPlayer attendant);
        GenericPlayer LeaveRoom(string attendant, out bool roomShouldDelete);
    }
}
