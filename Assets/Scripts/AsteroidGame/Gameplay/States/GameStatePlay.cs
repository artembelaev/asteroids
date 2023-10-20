
using AsteroidGame.Settings;
using AsteroidGame.System.States;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace AsteroidGame
{
    public class GameStatePlay : State<GameStateEnum>
    {
        private readonly Spaceship _player;
        private readonly IAsteroidsFactory _asteroidsFactory;
        private readonly IBalanceSettings _balanceSettings;

        public GameStatePlay(Spaceship player, IAsteroidsFactory asteroidsFactory, BalanceSettings balanceSettings)
            : base(GameStateEnum.Play)
        {
            _player = player;
            _asteroidsFactory = asteroidsFactory;
            _balanceSettings = balanceSettings;

            _player.OnKill += OnPLayerKill;
        }

        public override void OnEnter()
        {
            CreateAsteroids();
        }

        public override void OnExit()
        {
            Clear();
        }

        private void CreateAsteroids()
        {
            Clear();
            for (int i = 0; i < _balanceSettings.AsteroidsCount; ++i)
            {
                Vector2 position = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * 5f;;
                var asteroid = _asteroidsFactory.Create(0, position);
                if (asteroid != null)
                {
                    asteroid.Velocity = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * 3f;
                }
            }
        }

        private void OnPLayerKill(Character character)
        {
            Debug.Log($"---> Game Over"); // TODO go to scores state
        }

        private void Clear()
        {
            _asteroidsFactory.ClearAll();
        }
    }
}