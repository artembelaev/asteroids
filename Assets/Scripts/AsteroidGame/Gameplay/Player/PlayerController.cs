using UnityEngine;

namespace AsteroidGame
{
    public class PlayerController : CharacterController
    {
        [SerializeField] protected float _acceleration = Spaceship.DEFAULT_ACCELERATION;
        [SerializeField] private float _rotationSpeed = -90f;

        private Spaceship _spaceship;

        protected override object CreateModel()
        {
            return new Spaceship(_acceleration, transform.position,
                transform.rotation.z, _velocity, _maxVelocity);
        }

        protected override void Awake()
        {
            base.Awake();
            _spaceship = GetModel<Spaceship>();
        }

        protected override void Update()
        {
            bool engineEnabled = Input.GetKey(KeyCode.UpArrow); // TODO using Input System
            float rotateAxis = Input.GetAxis("Horizontal");   // TODO using Input System

            _spaceship.EnableEngine(engineEnabled);
            _spaceship.Rotation += _rotationSpeed * rotateAxis * Time.deltaTime;

            base.Update();
        }

    }
}