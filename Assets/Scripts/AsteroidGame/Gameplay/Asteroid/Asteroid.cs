using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AsteroidGame
{
    public class Asteroid : Character
    {
        public const float CHILD_MAX_ANGLE = 90f;

        public int Level { get; set; } = 0;
        public int LevelCount { get; set; } = 3;
        public int ChildCount { get; set; } = 2;
        public float ChildMaxAngleScatter { get; set; } = CHILD_MAX_ANGLE;

        /// <summary>
        /// Vector2 - velocity of new child
        /// </summary>
        public event Action<Vector2> OnCreateChild;

        public Asteroid(int level,
            Vector2 position = default,
            float rotation = 0f,
            Vector2 velocity = default,
            float maxVelocity = MAX_VELOCITY_DEFAULT) :
            base(position, rotation, velocity, maxVelocity)
        {
            Level = level;
            OnKill += CreateChildren;
        }

        private void CreateChildren(Character character)
        {
            if (Level >= LevelCount - 1)
                return;

            float halfRange = ChildMaxAngleScatter / 2f;
            for (int i = 0; i < ChildCount; ++i)
            {
                var childVelocityRotation = Quaternion.Euler(0f, 0f, Random.Range(-halfRange, halfRange));
                Vector2 childVelocity = childVelocityRotation * Velocity;
                OnCreateChild?.Invoke(childVelocity);
            }
        }
    }
}