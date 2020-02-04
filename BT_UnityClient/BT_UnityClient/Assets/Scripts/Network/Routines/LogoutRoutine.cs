using Account;
using Authentication;
using Network.Packets;

namespace Network.Routines
{
    public static class LogoutRoutine
    {
        public static void Execute(ref MyAccount account, ref DefaultPacket packet)
        {
            account.Deinitialize();
            LogoutEvent.Logout();
        }
    }
}
