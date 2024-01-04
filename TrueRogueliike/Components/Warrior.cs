using TrueRogueliike.Core.Interfaces;

namespace TrueRogueliike.Components
{
    public sealed class Warrior : GameEnemy
    {
        private readonly Random _random;
        private readonly IGameSceneReader _sceneReader;

        public Warrior(char symbol, VectorPosition position, int health, IGameSceneReader sceneReader, Random random)
             : base(symbol, position, health, sceneReader)
        {
            _random = random;
            _sceneReader = sceneReader;
        }

        public override void Update()
        {
            var objectsAround = _sceneReader.GetObjectsAround(Position);
            Player? player = objectsAround.OfType<Player>().FirstOrDefault();

            if (player != null)
            {
                Attack(player);
                return;
            }

            var offsets = new List<VectorPosition>
            {
                new VectorPosition(0, -1),
                new VectorPosition(0, 1),
                new VectorPosition(-1, 0),
                new VectorPosition(1, 0)
            };

            var freePositions = offsets
                .Where(offset => !objectsAround.Any(obj => obj.Position.Equals(Position + offset)))
                .ToList();

            if (freePositions.Any())
            {
                VectorPosition direction = freePositions[_random.Next(freePositions.Count)];
                Move(direction);
            }
        }

        public static void Attack(Player player)
        {
            player.TakeDamage(10);
        }
    }
}
