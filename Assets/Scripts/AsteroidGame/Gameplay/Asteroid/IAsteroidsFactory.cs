using UnityEngine;

namespace AsteroidGame
{
    public interface IAsteroidsFactory
    {
        Asteroid Create(int level, Vector2 position);
        void ClearAll();
    }
}