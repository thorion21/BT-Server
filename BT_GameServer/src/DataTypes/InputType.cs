using BT_GameServer.Interfaces;
using BT_GameServer.libs.Serialization;

namespace BT_GameServer.DataTypes
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