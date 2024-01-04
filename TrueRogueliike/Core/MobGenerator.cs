using TrueRogueliike.Components;
using TrueRogueliike.Core.Interfaces;

namespace TrueRogueliike.Core
{
    public class MobGenerator
    {
        private readonly GameObjectFactory _factory;
        private readonly IGameSceneReader _sceneReader;
        private readonly Random _random;

        public MobGenerator(GameObjectFactory factory, IGameSceneReader sceneReader, Random random)
        {
            _factory = factory;
            _sceneReader = sceneReader;
            _random = random;
        }

        public void GenerateMobs(int numWarriors, int numArchers)
        {
            GenerateWarriors(numWarriors);
            GenerateArchers(numArchers);
        }

        private void GenerateWarriors(int numWarriors)
        {
            for (int i = 0; i < numWarriors; i++)
            {
                VectorPosition position;
                do
                {
                    position = new VectorPosition(_random.Next(5, _sceneReader.Width), _random.Next(5, _sceneReader.Height));
                }
                while (!_sceneReader.IsPositionFree(position));

                _factory.CreateWarrior(position, _sceneReader, _random);
            }
        }

        private void GenerateArchers(int numArchers)
        {
            for (int i = 0; i < numArchers; i++)
            {
                VectorPosition position;
                do
                {
                    position = new VectorPosition(_random.Next(5, _sceneReader.Width), _random.Next(5, _sceneReader.Height));
                }
                while (!_sceneReader.IsPositionFree(position));

                _factory.CreateArcher(position, _sceneReader, _random);
            }
        }
    }
}
