using ProtoBuf;

namespace ProtobufTest
{
    [ProtoContract]
    public class ObjectMS
    {
        [ProtoMember(1)]
        public uint tick { get; set; }
        [ProtoMember(2)]
        public string ign { get; set; }

        [ProtoMember(3)]
        public DynamicContext myclass { get; set; }
    }
    
    
    [ProtoContract]
    public class DynamicContext
    {
        [ProtoMember(1)]
        public uint number { get; set; }
        [ProtoMember(2)]
        public byte length { get; set; }
    }
}