using System;
using JetBrains.Annotations;
using UnityEngine;

namespace AsteroidGame
{
    public class WeaponController : EntityController
    {
        public enum ProjectileType
        {
            SpawnCharacter,
            SpawnGameObject,
        }

        [SerializeField] private CharacterController _attachedCharacterController;
        [SerializeField] private int _ammoCount = 4;
        [SerializeField] private float _fireCooldown = 0.125f;
        [SerializeField] private ProjectileType _projectileType = ProjectileType.SpawnCharacter;

        [SerializeField] private CharacterController _projectilePrefabCharacter;
        [SerializeField] private GameObject _projectilePrefabGameObject;
        [SerializeField] private bool _spawnAsChild = true;
        [SerializeField] private Transform _spawnPosition;

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
            _spawnPosition = transform;
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

            Transform parent = _spawnAsChild ? transform : null;
            Vector3 position = _spawnPosition.position;
            switch (_projectileType)
            {
                case ProjectileType.SpawnCharacter:
                    SpawnProjectileCharacter(parent, position);
                    break;
                case ProjectileType.SpawnGameObject:
                    SpawnProjectileGameObject(parent, position);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void SpawnProjectileCharacter(Transform parent, Vector3 position)
        {
            var rotation = Quaternion.Euler(0f, 0f, AttachedCharacter.Rotation);
            CharacterController projectileController =
                Instantiate(_projectilePrefabCharacter, position, rotation, parent); // TODO pool
            Character projectile = projectileController.Character;
            projectile.Velocity = (rotation * Vector3.up) * projectile.Velocity.magnitude;
        }


        private void SpawnProjectileGameObject(Transform parent, Vector3 position)
        {
            var rotation = Quaternion.Euler(0f, 0f, AttachedCharacter.Rotation);
            Instantiate(_projectilePrefabGameObject, position, rotation, parent); // TODO pool

        }

    }
}