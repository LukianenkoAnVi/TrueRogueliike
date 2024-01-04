using TrueRogueliike.Core.Interfaces;

namespace TrueRogueliike.Components
{
    public class Arrow : GameEnemy
    {
        private readonly VectorPosition _direction;
        private readonly IGameSceneReader _sceneReader;

        public Arrow(char symbol, VectorPosition position, VectorPosition direction, IGameSceneReader sceneReader, int health = 1)
            : base(symbol, position, health, sceneReader)
        {
            _direction = direction;
            _sceneReader = sceneReader;
        }

        public override void Update()
        {
            var nextPosition = Position + _direction;

            var objectAtNextPosition = _sceneReader.GetObjectAtPosition(nextPosition);

            if (objectAtNextPosition == null)
            {
                Position = nextPosition;
                return;
            }

            if (objectAtNextPosition is Player player)
            {
                player.TakeDamage(10);
            }

            TakeDamage(Health);

        }
    }
}
