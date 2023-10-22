using System;
using UnityEngine;

namespace AsteroidGame
{
    public class WeaponReloaderController : EntityController
    {
        [SerializeField] private WeaponController _weaponController;
        [SerializeField] private int _maxAmmo = 4;
        [SerializeField] private float _reloadSpeed = 1f;

        private WeaponReloader _weaponReloader;

        protected override object CreateModel()
        {
            return new WeaponReloaderPerOneAmmo
            {
                MaxAmmo = _maxAmmo,
                ReloadSpeed = _reloadSpeed,
            };
        }

        protected override void Awake()
        {
            base.Awake();
            _weaponReloader = GetModel<WeaponReloader>();
        }

        private void Reset()
        {
            _weaponController = GetComponent<WeaponController>();
        }

        private void Start()
        {
            if (_weaponController != null)
                _weaponReloader.Weapon = _weaponController.Weapon;
        }
    }
}