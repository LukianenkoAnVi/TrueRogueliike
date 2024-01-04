using TrueRogueliike.Components;
using TrueRogueliike.Core.Interfaces;

namespace TrueRogueliike.Core
{
    public class GameObjectFactory
    {
       
        public delegate void GameObjectCreatedHandler(GameObject gameObject);
        public event GameObjectCreatedHandler OnGameObjectCreated = delegate { };

        public Player CreatePlayer(VectorPosition position, IGameSceneReader sceneReader)
        {
            var player = new Player('P', position, 50, sceneReader);
            OnGameObjectCreated?.Invoke(player);
            return player;
        }

        public void CreateWarrior(VectorPosition position, IGameSceneReader sceneReader, Random random)
        {
            var warrior = new Warrior('W', position, 100, sceneReader, random);
            OnGameObjectCreated?.Invoke(warrior);
        }

        public Archer CreateArcher(VectorPosition position, IGameSceneReader sceneReader, Random random)
        {
            var archer = new Archer('A', position, 80, sceneReader, random);
            archer.OnCreateArrow += CreateArrow;
            OnGameObjectCreated?.Invoke(archer);
            return archer;
        }

        private void CreateArrow(VectorPosition position, VectorPosition direction, IGameSceneReader sceneReader)
        {
            var arrowSymbol = direction.Y != 0 ? '|' : '-';
            var arrow = new Arrow(arrowSymbol, position, direction, sceneReader);
            OnGameObjectCreated?.Invoke(arrow);
        }

        public void UnsubscribeArcher(Archer archer)
        {
            archer.OnCreateArrow -= CreateArrow;
        }

        public void CreateWall(VectorPosition position)
        {
            var wall = new GameObject('#', position);
            OnGameObjectCreated?.Invoke(wall);
        }
    }
}
