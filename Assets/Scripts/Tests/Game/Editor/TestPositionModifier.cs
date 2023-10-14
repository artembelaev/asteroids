using System;
using Game;
using UnityEngine;


public class TestPositionModifier : IPositionModifier
{
    public Vector2 ModifiedPosition;
    public bool Modified;

    public Vector2 Modify(Vector2 position)
    {
        Modified = true;
        return ModifiedPosition;
    }
}