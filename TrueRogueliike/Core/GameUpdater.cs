using TrueRogueliike.Components;
using TrueRogueliike.Core.Interfaces;

namespace TrueRogueliike.Core
{
    public class GameUpdater
    {
        public static void Update(ref bool isRunning, IGameSceneEditor scene)
        {
            var gameObjectsCopy = new List<GameObject>(scene.GameObjects);

            foreach (var gameObject in gameObjectsCopy)
            {
                gameObject.Update();

                if (gameObject is GameEnemy enemy && enemy.Health < 1)
                {
                    scene.RemoveGameObject(enemy);
                }

                if (gameObject is Player player)
                {
                    if (player.Health < 1) 
                    {
                        isRunning = false;
                    }

                    if (player.Position.X == scene.Width - 2 && player.Position.Y == scene.Height - 2)
                    {
                        scene.Finished();
                    }
                }
            }
        }
    }
}
