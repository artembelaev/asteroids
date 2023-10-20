using System;
using System.Threading.Tasks;
using UnityEngine;

namespace AsteroidGame
{
    public class Player : Spaceship
    {
        public float BlinkDuration { get; set; } = 2f;

        public int LivesCount { get; set; } = 3;

        public float RespawnDelay { get; set; } = 2f;

        public Vector2 RespawnPosition { get; set; }
        public bool NeedRespawn { get; set; }

        public event Action<Player, Vector2> OnRespawn;

        private float _blinkTimeRemains;
        private float _timeToRespawn;

        public override void Kill()
        {
            if (IsKilled)
                return;

            if (LivesCount > 0)
                --LivesCount;

            if (LivesCount > 0)
                RespawnAfterDelay();
            else
                base.Kill();
        }

        private void RespawnAfterDelay()
        {
            NeedRespawn = true;
            _timeToRespawn = RespawnDelay;
        }

        public void Respawn()
        {
            Respawn(RespawnPosition);
        }

        public void Respawn(Vector2 position)
        {
            NeedRespawn = false;
            IsBlink = true;
            _blinkTimeRemains = BlinkDuration;
            OnRespawn?.Invoke(this, position);
        }

        public override void Tick(float dt)
        {
            if (IsBlink)
            {
                _blinkTimeRemains -= dt;
                if (_blinkTimeRemains <= 0f)
                    IsBlink = false;
            }

            if (NeedRespawn)
            {
                _timeToRespawn -= dt;
                if (_timeToRespawn <= 0f)
                    Respawn();
            }

            base.Tick(dt);
        }
    }
}