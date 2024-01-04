using TrueRogueliike.Core.Interfaces;
using System;

namespace TrueRogueliike.Components
{
    public sealed class Archer : GameEnemy
    {
        public event Action<VectorPosition, VectorPosition, IGameSceneReader>? OnCreateArrow;

        private readonly Random _random;
        private readonly IGameSceneReader _sceneReader;

        public Archer(char symbol, VectorPosition position, int health, IGameSceneReader sceneReader, Random random)
             : base(symbol, position, health, sceneReader)
        {
            _random = random;
            _sceneReader = sceneReader;
        }

        public override void Update()
        {
            var firstObjectsInEachDirection = _sceneReader.GetFirstObjectsInEachDirection(Position);
            
            foreach (var (direction, obj) in firstObjectsInEachDirection)
            {
                if (obj is Player player)
                {
                    var attackDirection = GetDirectionForAttack(direction);
                    if (_sceneReader.IsPositionFree(Position + attackDirection))
                    {
                        Attack(Position + attackDirection, attackDirection);
                        return;
                    }
                }
            }

            var objectsAround = _sceneReader.GetObjectsAround(Position);
            var freePositions = new List<VectorPosition>
            {
                new VectorPosition(0, -1),
                new VectorPosition(0, 1),
                new VectorPosition(-1, 0),
                new VectorPosition(1, 0)
            }.Where(offset => !objectsAround.Any(obj => obj.Position.Equals(Position + offset)))
                .Select(offset => Position + offset)
                .ToList();

            if (freePositions.Any())
            {
                VectorPosition direction = freePositions[_random.Next(freePositions.Count)] - Position;
                Move(direction);
            }
        }

        public void Attack(VectorPosition startPosition, VectorPosition direction)
        {
            OnCreateArrow?.Invoke(startPosition, direction, _sceneReader);
        }

        private static VectorPosition GetDirectionForAttack(string directionKey)
        {
            return directionKey switch
            {
                "Up" => new VectorPosition(0, -1),
                "Down" => new VectorPosition(0, 1),
                "Left" => new VectorPosition(-1, 0),
                "Right" => new VectorPosition(1, 0),
                _ => new VectorPosition(0, 0),
            };
        }
    }
}
