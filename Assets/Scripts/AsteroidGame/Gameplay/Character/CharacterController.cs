using UnityEngine;

namespace AsteroidGame
{
    public class CharacterController : EntityController
    {
        [SerializeField] protected Vector2 _velocity;
        [SerializeField] protected float _maxVelocity = Character.MAX_VELOCITY_DEFAULT;

        private Character _character;

        public Character Character => _character;

        protected override object CreateModel()
        {
            return new Character(transform.position, transform.rotation.z, _velocity, _maxVelocity);
        }

        protected override void Awake()
        {
            base.Awake();
            _character = GetModel<Character>();
        }

        protected override void Update()
        {
            base.Update();
            transform.position = _character.Position;
            transform.rotation = Quaternion.Euler(0f, 0f, _character.Rotation);
        }
    }
}