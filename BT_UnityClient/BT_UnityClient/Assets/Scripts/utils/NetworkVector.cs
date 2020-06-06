using MessagePack;
using UnityEngine.Networking;

namespace utils
{
    [MessagePackObject]
    public class NetworkVector
    {
        [Key(0)] public float x;
        [Key(1)] public float y;
        [Key(2)] public float z;
        
        [SerializationConstructor]
        public NetworkVector(float x=0f, float y=0f, float z=0f)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        
        public static NetworkVector operator +(NetworkVector a, NetworkVector b) => new NetworkVector(a.x + b.x, a.y + b.y, a.z + b.z);
        public static NetworkVector operator -(NetworkVector a, NetworkVector b) => new NetworkVector(a.x - b.x, a.y - b.y, a.z - b.z);
        public static NetworkVector operator *(NetworkVector a, float b) => new NetworkVector(a.x * b, a.y * b, a.z * b);

        public byte[] AsByteArray()
        {
            return MessagePackSerializer.Serialize(this);
        }
        
        public static NetworkVector Deserialize(byte[] bytes)
        {
            return MessagePackSerializer.Deserialize<NetworkVector>(bytes);
        }
    }
}