using TrueRogueliike.Core.Interfaces;

namespace TrueRogueliike.Components
{
    public class GameEnemy : GameObject
    {
        private readonly IGameSceneReader _sceneReader;
        public int Health { get; protected set; }

        public GameEnemy(char symbol, VectorPosition position, int health, IGameSceneReader sceneReader)
            : base(symbol, position)
        {
            Health = health;
            _sceneReader = sceneReader;
        }

        public virtual void Move(VectorPosition direction)
        {
            VectorPosition newPosition = Position + direction;

            if (_sceneReader.IsPositionFree(newPosition))
            {
                Position = newPosition;
            }
        }

        public void TakeDamage(int damageAmount)
        {
            Health -= damageAmount;
        }
        public void AddHealth(int damageAmount)
        {
            Health += damageAmount;
        }
    }
}
