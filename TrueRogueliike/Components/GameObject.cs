namespace TrueRogueliike.Components
{
    public class GameObject
    {
        public char Symbol { get; protected set; }
        public VectorPosition Position { get; protected set; }

        public GameObject(char symbol, VectorPosition position)
        {
            Symbol = symbol;
            Position = position;
        }

        public virtual void Update() {}
    }
}