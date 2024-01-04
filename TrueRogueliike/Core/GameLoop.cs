using TrueRogueliike.Components;

namespace TrueRogueliike.Core
{
    public class GameLoop
    {
        private readonly GameScene _scene;
        private readonly Player _player;

        public GameLoop(GameScene scene, Player player)
        {
            _scene = scene;
            _player = player;
        }

        public void Run()
        {
            bool isRunning = true;

            GameRenderer.Render(_scene);

            while (isRunning)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);

                    if (keyInfo.Key == ConsoleKey.Escape)
                    {
                        isRunning = false;
                    }

                    PlayerController.Update(keyInfo.Key, _player);
                    GameUpdater.Update(ref isRunning, _scene);

                    GameRenderer.Render(_scene);
                }
            }

            _scene.Unsubscribe();
        }
    }
}
