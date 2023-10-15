using UnityEngine;

namespace AsteroidGame.Settings
{
    [CreateAssetMenu(menuName = "Settings/Balance", fileName = nameof(BalanceSettings))]
    public class BalanceSettings : ScriptableObject
    {
        public int AsteroidsCount = 6;
        public int AsteroidsLevels = 3;
    }
}