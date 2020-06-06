using Account;
using UI;
using UnityEngine;
using utils;

namespace Network.Packets.Instance
{
    public static class InstanceReadyHandler
    {
        private static MyAccount _account = MyAccount.Instance;
        private static GameLogic _gameLogic = GameLogic.Instance;
        
        public static void Handle(ref DefaultPacket packet)
        {
            GSInstanceReady response = GSInstanceReady.Deserialize(packet.Buffer);
            
            DefaultPacket confirmIdentityPkt = GetConfirmIdentityMessage(response.id);
            _gameLogic.SendGS(ref confirmIdentityPkt);
            Debug.Log("ConfirmIdentity sent to GS");
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