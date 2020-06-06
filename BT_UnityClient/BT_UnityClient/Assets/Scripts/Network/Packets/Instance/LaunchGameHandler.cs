using System;
using System.Diagnostics;
using System.Net.NetworkInformation;
using Account;
using UI;
using utils;
using Debug = UnityEngine.Debug;

namespace Network.Packets.Instance
{
    public static class LaunchGameHandler
    {
        private static UiManager _ui = UiManager.Instance;
        private static MyAccount _account = MyAccount.Instance;
        private static bool alreadySet = false;
        
        public static void Handle(ref DefaultPacket packet, ref GameInstance gameInstance)
        {
            if (alreadySet)
                return;
            
            /* Add players from packets and create capsules with them */
            GSGameStartPacket response = GSGameStartPacket.Deserialize(packet.Buffer);
            alreadySet = true;
            Debug.Log("A INTRAAAAAAAAAAAAAAAAAAAAAAAAT  " + response.instance_id);
            
            gameInstance.Signal(GameEvents.NotifyName, _account.GetIGN());
            gameInstance.Signal(GameEvents.NotifyInstance, response.instance_id);
            gameInstance.Signal(GameEvents.SpawnPlayers, response.players);

            Debug.Log("Cica am si apelat...");
            
            _ui.Signal(UiEvents.GameStart);
        }
    }
}