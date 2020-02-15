using Account;
using Network.Packets;
using utils;

namespace Network.Routines
{
    public static class LogoutRoutine
    {
        public static bool Execute(ref MyAccount account, ref DefaultPacket packet)
        {
            LogoutPacketResponse response = LogoutPacketResponse.Deserialize(packet.Buffer);

            if (response.Status == LoginStatus.LOGOUT_ACCEPTED)
            {
                account.Deinitialize();
                return true;
            }

            return false;
        }
    }
}
