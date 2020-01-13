using BT_Server.Interfaces;
using BT_Server.libs.Serialization;

namespace BT_Server.DataTypes
{
    /*
     * Only player actions, not key presses
     * Keypresses are processed locally and adequate packets are sent
     */
    public class InputType : IData
    {
        public uint Peer;
        public string Name;
        public bool Forward;
        public bool Backward;
        public bool Left;
        public bool Right;
        public bool LeftButton;
        public bool RightButton;
        public bool Jump;

        public InputType(ref BitBuffer data)
        {
            Peer = data.ReadUInt();
            Name = data.ReadString();
            Forward = data.ReadBool();
            Backward = data.ReadBool();
            Left = data.ReadBool();
            Right = data.ReadBool();
            LeftButton = data.ReadBool();
            RightButton = data.ReadBool();
            Jump = data.ReadBool();
            
            data.Clear();
        }
    }
}