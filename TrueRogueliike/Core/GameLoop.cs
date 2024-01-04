namespace TrueRogueliike.Core
{
    public class GameLoop
    {
        private readonly GameScene _scene;
        private readonly PlayerController _playerController;
        private readonly GameUpdater _gameUpdater;

        public GameLoop(GameScene scene, PlayerController playerController)
        {
            _scene = scene;
            _playerController = playerController;
            _gameUpdater = new GameUpdater(_scene);
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

                    _playerController.Update(keyInfo.Key);

                    _gameUpdater.Update(ref isRunning);
                    GameRenderer.Render(_scene);
                }
            }

            _scene.Unsubscribe();
        }
    }
}
