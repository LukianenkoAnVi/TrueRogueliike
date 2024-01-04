using TrueRogueliike.Components;

namespace TrueRogueliike.Core
{
    public class MazeGenerator
    {
        private readonly GameObjectFactory _factory;
        private readonly Random _random;
        private readonly int _width, _height;
        private readonly int[,] _maze;

        public MazeGenerator(GameObjectFactory factory, int width, int height, Random random)
        {

            _factory = factory;
            _width = width;
            _height = height;
            _maze = new int[width, height];
            _random = random;
        }

        public void GenerateMaze()
        {
            InitializeMaze();
            RecursiveBacktrack(1, 1);
            EnsureExit();
            AddWallsToScene();
        }

        private void InitializeMaze()
        {
            for (int x = 0; x < _width; x++)
                for (int y = 0; y < _height; y++)
                    _maze[x, y] = 1;
        }

        private void RecursiveBacktrack(int x, int y)
        {
            _maze[x, y] = 0;

            VectorPosition[] directions = { new VectorPosition(1, 0), new VectorPosition(-1, 0), new VectorPosition(0, 1), new VectorPosition(0, -1) };

            for (int i = 0; i < directions.Length; i++)
            {
                int swapIndex = _random.Next(i, directions.Length);
                (directions[swapIndex], directions[i]) = (directions[i], directions[swapIndex]);
            }

            foreach (var direction in directions)
            {
                if (x + direction.X * 2 > 0 
                    && x + direction.X * 2 < _width - 1 
                    && y + direction.Y * 2 > 0 
                    && y + direction.Y * 2 < _height - 1 
                    && _maze[x + direction.X * 2, y + direction.Y * 2] == 1)
                {
                    _maze[x + direction.X, y + direction.Y] = 0;
                    RecursiveBacktrack(x + direction.X * 2, y + direction.Y * 2);
                }
            }
        }

        private void EnsureExit()
        {
            _maze[_width - 2, _height - 2] = 0;
            _maze[_width - 2, _height - 3] = 0;
        }

        private void AddWallsToScene()
        {
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    if (_maze[x, y] == 1)
                        _factory.CreateWall(new VectorPosition(x, y));
                }
            }
        }
    }
}
