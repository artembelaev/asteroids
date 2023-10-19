
using AsteroidGame.Settings;
using AsteroidGame.System.States;
using UnityEngine;

namespace AsteroidGame
{
    public class GameStatePlay : State<GameStateEnum>
    {
        private readonly IAsteroidsFactory _asteroidsFactory;
        private readonly BalanceSettings _balanceSettings;

        public GameStatePlay(IAsteroidsFactory asteroidsFactory, BalanceSettings balanceSettings) : base(GameStateEnum.Play)
        {
            _asteroidsFactory = asteroidsFactory;
            _balanceSettings = balanceSettings;
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
                asteroid.Velocity = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * 3f;
            }
        }

        private void Clear()
        {
            _asteroidsFactory.ClearAll();
        }
    }
}