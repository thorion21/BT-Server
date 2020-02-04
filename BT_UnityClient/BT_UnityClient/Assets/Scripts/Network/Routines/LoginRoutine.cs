using Account;
using Authentication;
using Network.Packets;
using UnityEngine;
using utils;

namespace Network.Routines
{
    public static class LoginRoutine
    {
        public static bool Check(ref MyAccount account, ref DefaultPacket packet)
        {
            LoginPacketResponse response = LoginPacketResponse.Deserialize(packet.Buffer);

            if (response.Status == LoginStatus.LOGIN_SUCCESS)
            {
                LoginSuccessful.StartRoutine();
                return account.Initialize(response.IGN, response.Token);
            }

            if (response.Status == LoginStatus.LOGIN_FAILED)
            {
                Debug.Log("Login failed. Wrong credentials.");
            }

            return false;
        }
    }
}