using System;
using UnityEngine;

namespace AsteroidGame
{
    public class Spaceship : Character
    {
        public const float DEFAULT_ACCELERATION = 5f;

        public float Acceleration { get; set; } = DEFAULT_ACCELERATION;
        public bool EngineEnabled { get; private set; }

        public event Action<bool> OnEngineEnabled;

        private float _currAcceleration = 0f;

        public void EnableEngine(bool enabled)
        {
            EngineEnabled = enabled;
            _currAcceleration = enabled ? Acceleration : 0f;
            OnEngineEnabled?.Invoke(enabled);
        }

        protected override void UpdatePosition(float dt)
        {
            Vector2 accelVector = GetAccelVector();
            Velocity += accelVector * dt;
            base.UpdatePosition(dt);
        }

        private Vector3 GetAccelVector()
        {
            return Quaternion.Euler(0f, 0f, Rotation) * Vector2.up * _currAcceleration;
        }
    }
}