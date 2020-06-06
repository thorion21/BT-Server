using System.Threading;
using Account;
using UI;
using UnityEngine;
using utils;

namespace Network.Packets.Instance
{
    public static class LaunchInstanceHandler
    {
        private static MyAccount _account = MyAccount.Instance;
        private static GameServerComm _gsComm = GameServerComm.Instance;
        private static GameLogic _gameLogic = GameLogic.Instance;
        public static void Handle(ref DefaultPacket packet)
        {
            LaunchInstancePacket response = LaunchInstancePacket.Deserialize(packet.Buffer);
            
            Debug.Log(response.GameServerIP + " and " + response.GameServerPORT);
            /* Start GS communication thread */
            _gsComm.SetAndRunServerHost(response.GameServerIP, response.GameServerPORT);
            Debug.Log("DE CE MORTII MATII AI INTRAT DE 2 ORI?!?!?");
            
            /* Change scene to game mode and select new basic sending queue to be to GS */
            _account.SetCurrentInstance(response.InstanceID);
            
            DefaultPacket confirmIdentityPkt = GetConfirmIdentityMessage(response.InstanceID); 
            _gameLogic.SendGS(ref confirmIdentityPkt);
            Debug.Log("Confirm sent to GS");
        }

        private static DefaultPacket GetConfirmIdentityMessage(int instance_id)
        {
            DefaultPacket response;
            
            response = new DefaultPacket(
                PacketType.GS_CONFIRM_IDENTITY,
                new GSConfirmIdentityPacket(
                    instance_id,
                    _account.GetIGN()
                ).AsByteArray()
            );

            return response;
        }
    }
}