using TrueRogueliike.Core.Interfaces;

namespace TrueRogueliike.Components
{
    public sealed class Player : GameEnemy
    {

        public Player(char symbol, VectorPosition position, int health, IGameSceneReader sceneReader)
             : base(symbol, position, health, sceneReader) { }

        public void SetStartPosition()
        {
            Position = new(1, 1);
        }
    }
}