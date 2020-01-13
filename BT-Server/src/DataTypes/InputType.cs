namespace BT_Server.DataTypes
{
    /*
     * Only player actions, not key presses
     * Keypresses are processed locally and adequate packets are sent
     */
    public struct InputType
    {
        public bool Forward;
        public bool Backward;
        public bool Left;
        public bool Right;
        public bool LeftButton;
        public bool RightButton;
        public bool Jump;
    }
}