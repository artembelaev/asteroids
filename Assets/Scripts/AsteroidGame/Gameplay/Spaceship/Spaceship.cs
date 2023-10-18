using UnityEngine;

namespace AsteroidGame
{
    public class Spaceship : Character
    {
        public const float DEFAULT_ACCELERATION = 5f;

        public float Acceleration { get; private set; }
        public bool EngineEnabled { get; private set; }

        private float _currAcceleration = 0f;

        public Spaceship(
            float acceleration = DEFAULT_ACCELERATION,
            Vector2 position = default,
            float rotation = 0f,
            Vector2 velocity = default,
            float maxVelocity = MAX_VELOCITY_DEFAULT):
            base(position, rotation, velocity, maxVelocity)
        {
            Acceleration = acceleration;
        }

        public void EnableEngine(bool enabled)
        {
            EngineEnabled = enabled;
            _currAcceleration = enabled ? Acceleration : 0f;
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