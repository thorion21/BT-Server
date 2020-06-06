using Account;
using utils;

namespace Network.Packets.Instance
{
    public static class GSUpdateHealth
    {
        private static MyAccount _account = MyAccount.Instance;
        public static void Handle(ref DefaultPacket packet, ref GameInstance gameInstance)
        {
            GSUpdateHealthPacket response = GSUpdateHealthPacket.Deserialize(packet.Buffer);

            gameInstance.Signal(GameEvents.HealthUpdate, response.amount);
        }
    }
}