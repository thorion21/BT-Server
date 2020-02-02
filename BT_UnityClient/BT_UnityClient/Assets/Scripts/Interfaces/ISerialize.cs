using MessagePack;

namespace Interfaces
{
    public interface ISerialize
    {
        byte[] AsByteArray();
    }
}