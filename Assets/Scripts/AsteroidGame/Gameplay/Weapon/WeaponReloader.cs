using System;
using JetBrains.Annotations;
using UnityEngine;

namespace AsteroidGame
{
    public class WeaponReloader : Entity
    {
        [CanBeNull] public Weapon Weapon { get; set; }
        public int MaxAmmo { get; set; }

        public event Action<Weapon, int> OnReload;

        protected void OnReloadEvent(int ammoCount)
        {
            if (Weapon != null)
                OnReload?.Invoke(Weapon, ammoCount);
        }
    }

    public class WeaponReloaderPerOneAmmo : WeaponReloader
    {
        public float ReloadSpeed { get; set; } // ammo per second

        private float _reloadRatio;

        public override void Tick(float dt)
        {
            base.Tick(dt);
            if (Weapon == null)
                return;

            if (Weapon.AmmoCount >= MaxAmmo)
            {
                _reloadRatio = 0f;
                return;
            }

            _reloadRatio += dt * ReloadSpeed;
            if (_reloadRatio >= 1)
            {
                int ammoCount = Mathf.FloorToInt(_reloadRatio);
                _reloadRatio -= ammoCount;
                Weapon.AmmoCount += ammoCount;
                OnReloadEvent(ammoCount);
            }

        }
    }
}