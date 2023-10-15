using UnityEngine;

namespace AsteroidGame
{
    public interface IPositionModifier
    {
        Vector2 Modify(Vector2 position);
    }
}