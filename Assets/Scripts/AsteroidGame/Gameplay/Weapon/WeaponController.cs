using System;
using JetBrains.Annotations;
using UnityEngine;

namespace AsteroidGame
{
    public class WeaponController : EntityController
    {
        [SerializeField] private int _ammoCount = 4;
        [SerializeField] private float _fireCooldown = 0.125f;
        [SerializeField] private CharacterController _projectilePrefab;
        [SerializeField] private CharacterController _attachedCharacterController;

        private Weapon _weapon;

        [CanBeNull] public Character AttachedCharacter { get; set; }

        public Weapon Weapon => _weapon;

        protected override object CreateModel()
        {
            return new Weapon()
            {
                AmmoCount = _ammoCount,
                FireCooldown = _fireCooldown,
            };
        }

        private void Reset()
        {
            _attachedCharacterController = GetComponent<CharacterController>();
        }

        protected override void Awake()
        {
            base.Awake();
            _weapon = GetModel<Weapon>();
            _weapon.OnFire += OnFire;
        }

        private void Start()
        {
            if (_attachedCharacterController != null)
                AttachedCharacter = _attachedCharacterController.Character;
        }

        private void OnFire(Weapon weapon)
        {
            if (AttachedCharacter == null)
                return;

            var rotation = Quaternion.Euler(0f, 0f, AttachedCharacter.Rotation);
            CharacterController projectileController = Instantiate(_projectilePrefab, AttachedCharacter.Position, rotation); // TODO pool
            Character projectile = projectileController.Character;
            projectile.Velocity = (rotation * Vector3.up) * projectile.Velocity.magnitude;

        }

    }
}