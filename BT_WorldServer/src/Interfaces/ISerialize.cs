using MessagePack;

namespace BT_WorldServer.Interfaces
{
    public interface ISerialize
    {
        byte[] AsByteArray();
    }
}