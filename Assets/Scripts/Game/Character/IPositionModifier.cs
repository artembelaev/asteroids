using UnityEngine;

namespace Game
{
    public interface IPositionModifier
    {
        Vector2 Modify(Vector2 position);
    }
}