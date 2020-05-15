using UnityEngine;

namespace Network.Packets.Instance
{
    public static class LaunchInstanceHandler
    {
        public static void Handle(ref DefaultPacket packet)
        {
            LaunchInstancePacket response = LaunchInstancePacket.Deserialize(packet.Buffer);
            
            Debug.Log(response.GameServerIP + " and " + response.GameServerPORT);
        }
    }
}