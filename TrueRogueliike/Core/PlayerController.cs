using TrueRogueliike.Components;

namespace TrueRogueliike.Core
{
    using System.Collections.Generic;

    public class PlayerController
    {
        private readonly Player _player;

        private readonly Dictionary<ConsoleKey, VectorPosition> _directions = new()
        {
            {ConsoleKey.W, new VectorPosition(0, -1)},
            {ConsoleKey.S, new VectorPosition(0, 1)},
            {ConsoleKey.A, new VectorPosition(-1, 0)},
            {ConsoleKey.D, new VectorPosition(1, 0)}
        };

        public PlayerController(Player player)
        {
            _player = player;
        }

        public void Update(ConsoleKey key)
        {
            VectorPosition direction = _directions.TryGetValue(key, out var dir) ? dir : new VectorPosition(0, 0);

            _player.Move(direction);
        }
    }
}
