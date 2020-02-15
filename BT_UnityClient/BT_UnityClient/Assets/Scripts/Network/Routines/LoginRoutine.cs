using Account;
using Network.Packets;
using UI;
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