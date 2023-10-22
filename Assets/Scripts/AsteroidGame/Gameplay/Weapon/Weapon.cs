using System;
using UnityEngine;

namespace AsteroidGame
{
    public class Weapon : Entity
    {
        public int AmmoCount
        {
            get => _ammoCount;
            set
            {
                _ammoCount = value;
                OnAmmoCountChanged?.Invoke(this, value);
            }
        }

        public float FireCooldown { get; set; }

        public bool FireTrigger
        {
            get => _fireTrigger;
            set
            {
                if (value == _fireTrigger)
                    return;

                _fireTrigger = value;
                if (value)
                    Fire();
            }
        }

        public event Action<Weapon> OnFire;
        public event Action<Weapon, int> OnAmmoCountChanged;

        private bool _fireTrigger;
        private float _fireCooldownRemains;
        private int _ammoCount;

        public override void Tick(float dt)
        {
            base.Tick(dt);

            if (_fireCooldownRemains > 0f)
                _fireCooldownRemains -= dt;

            if (_fireTrigger)
                Fire();
        }

        public void Fire()
        {
            if (AmmoCount <= 0 || _fireCooldownRemains > 0f)
                return;

            _fireCooldownRemains = FireCooldown;
            --AmmoCount;
            OnFire?.Invoke(this);
        }

    }
}