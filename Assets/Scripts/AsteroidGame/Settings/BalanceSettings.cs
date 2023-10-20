using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace AsteroidGame.Settings
{
    public interface IBalanceSettings
    {
        int AsteroidsCount { get; }
        int GetScores(int asteroidLevel);
    }

    [CreateAssetMenu(menuName = "Settings/Balance", fileName = nameof(BalanceSettings))]
    public class BalanceSettings : ScriptableObject, IBalanceSettings
    {
        [SerializeField] private int _asteroidsCount = 6;
        [SerializeField] private List<int> _scoresForAsteroids = new() {20, 50, 100};

        public int AsteroidsCount => _asteroidsCount;

        public int GetScores(int asteroidLevel)
        {
            Assert.IsTrue(asteroidLevel < _scoresForAsteroids.Count);
            return _scoresForAsteroids[asteroidLevel];
        }
    }
}