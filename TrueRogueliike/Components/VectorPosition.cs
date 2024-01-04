namespace TrueRogueliike.Components
{
    public struct VectorPosition
    {
        public int X { get; set; }
        public int Y { get; set; }

        public VectorPosition(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static VectorPosition operator +(VectorPosition a, VectorPosition b)
        {
            return new VectorPosition(a.X + b.X, a.Y + b.Y);
        }

        public static VectorPosition operator -(VectorPosition a, VectorPosition b)
        {
            return new VectorPosition(a.X - b.X, a.Y - b.Y);
        }

    }
}
