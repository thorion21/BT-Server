using utils;

namespace Network.Packets.Instance
{
    public static class GSCorrectionsHandler
    {
        
        public static void Handle(ref DefaultPacket packet, ref GameInstance gameInstance)
        {
            GSCorrectionsPacket response = GSCorrectionsPacket.Deserialize(packet.Buffer);
            
            gameInstance.Signal(GameEvents.Correction, response);
        }
    }
}