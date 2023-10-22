using UnityEngine;

namespace AsteroidGame
{
    public class PlayerController : CharacterController
    {
        [SerializeField] protected float _acceleration = Spaceship.DEFAULT_ACCELERATION;
        [SerializeField] private float _rotationSpeed = -90f;
        [SerializeField] private int _livesCount = 3;
        [SerializeField] private float _blinkDuration = 2f;
        [SerializeField] private float _respawnDelay = 2f;
        [SerializeField] private WeaponController _bulletWeapon;
        [SerializeField] private WeaponController _laserWeapon;

        private Player _player;

        protected override object CreateModel()
        {
            return new Player
            {
                Position = transform.position, Rotation = transform.rotation.z,
                Velocity = _velocity, MaxVelocity = _maxVelocity,
                Acceleration = _acceleration,
                LivesCount = _livesCount,
                BlinkDuration = _blinkDuration,
                RespawnDelay = _respawnDelay,
            };
        }

        protected override void Awake()
        {
            base.Awake();
            _player = GetModel<Player>();
        }

        protected override void Update()
        {
            UpdateLaserWeapon();
            UpdateBulletWeapon();
            UpdateMovement();

            base.Update();
        }

        private void UpdateLaserWeapon()
        {
            bool fire = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl); // TODO using Input System
            _laserWeapon.Weapon.FireTrigger = fire;
        }

        private void UpdateBulletWeapon()
        {
            bool fire = Input.GetKey(KeyCode.Space); // TODO using Input System
            _bulletWeapon.Weapon.FireTrigger = fire;
        }

        private void UpdateMovement()
        {
            bool engineEnabled = Input.GetKey(KeyCode.UpArrow); // TODO using Input System
            float rotateAxis = Input.GetAxis("Horizontal"); // TODO using Input System

            _player.EnableEngine(engineEnabled);
            _player.Rotation += _rotationSpeed * rotateAxis * Time.deltaTime;
        }
    }
}