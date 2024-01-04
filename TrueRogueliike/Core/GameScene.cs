using TrueRogueliike.Components;
using TrueRogueliike.Core.Interfaces;

namespace TrueRogueliike.Core
{
    public class GameScene : IGameSceneReader, IGameSceneEditor
    {
        private List<GameObject> _gameObjects = new();
        public IReadOnlyList<GameObject> GameObjects => _gameObjects;

        public int Width { get; private set; }
        public int Height { get; private set; }

        private readonly GameObjectFactory _factory;
        private readonly Random _random;

        public GameScene(int width, int height, GameObjectFactory factory, Random random)
        {
            Width = width;
            Height = height;
            _gameObjects = new List<GameObject>(width * height);
            _random = random;
            _factory = factory;

            _factory.OnGameObjectCreated += AddGameObject;

            Init();
        }

        private void Init()
        {
            UnsubscribeAllArchers();
            _gameObjects.Clear();

            MazeGenerator generator = new(_factory, Width, Height, _random);
            MobGenerator generator2 = new(_factory, this, _random);

            generator.GenerateMaze();
            generator2.GenerateMobs(3, 2);
        }
        
        public void Finished()
        {
            var player = _gameObjects.OfType<Player>().FirstOrDefault();
            
            Init();

            if (player != null)
            {
                player.SetStartPosition();
                player.AddHealth(10);

                _gameObjects.Add(player);
            }
        }

        public void AddGameObject(GameObject gameObject)
        {
            if (GameObjects.Any(obj => obj != null && obj.Position.X == gameObject.Position.X && obj.Position.Y == gameObject.Position.Y))
            {
                throw new InvalidOperationException("Position is already occupied.");
            }

            _gameObjects.Add(gameObject);
        }

        public void RemoveGameObject(GameObject gameObject)
        {
            if (gameObject is Archer archer)
            {
                _factory.UnsubscribeArcher(archer);
            }

            _gameObjects.Remove(gameObject);
        }

        public bool IsPositionFree(VectorPosition position)
        {
            return !_gameObjects.Any(gameObject => gameObject.Position.Equals(position));
        }

        public List<GameObject> GetObjectsAround(VectorPosition position)
        {
            var offsets = new List<VectorPosition>
            {
                new VectorPosition(0, -1),
                new VectorPosition(0, 1), 
                new VectorPosition(-1, 0),
                new VectorPosition(1, 0)
            };

            return offsets
                .Select(offset => position + offset)
                .Where(pos => pos.X >= 0 && pos.X < Width && pos.Y >= 0 && pos.Y < Height)
                .SelectMany(pos => _gameObjects.Where(obj => obj.Position.Equals(pos)))
                .ToList();
        }

        public Dictionary<string, GameObject> GetFirstObjectsInEachDirection(VectorPosition position)
        {
            Dictionary<string, GameObject> firstObjects = new();
            
            var directions = new List<(string, VectorPosition)>
            {
                ("Up", new VectorPosition(0, -1)),
                ("Down", new VectorPosition(0, 1)),
                ("Left", new VectorPosition(-1, 0)),
                ("Right", new VectorPosition(1, 0))
            };

            foreach (var (name, offset) in directions)
            {
                VectorPosition currentPosition = position + offset;

                while (currentPosition.X >= 0 && currentPosition.X < Width && currentPosition.Y >= 0 && currentPosition.Y < Height)
                {
                    var obj = _gameObjects.FirstOrDefault(o => o.Position.Equals(currentPosition));
                    if (obj != null)
                    {
                        firstObjects[name] = obj;
                        break;
                    }
                    currentPosition += offset;
                }
            }

            return firstObjects;
        }

        public GameObject? GetObjectAtPosition(VectorPosition position)
        {
            return _gameObjects.FirstOrDefault(obj => obj.Position.Equals(position));
        }

        public void Unsubscribe()
        {
            _factory.OnGameObjectCreated -= AddGameObject;
            UnsubscribeAllArchers();
        }

        private void UnsubscribeAllArchers()
        {
            foreach (var archer in _gameObjects.OfType<Archer>())
            {
                _factory.UnsubscribeArcher(archer);
            }
        } 
    }
}
