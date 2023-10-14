using UnityEngine;

namespace Game
{
    public class CharacterControllerBehaviour : EntityControllerBehaviour
    {
        [SerializeField] private Vector2 _velocity;
        [SerializeField] private float _maxVelocity = Character.MAX_VELOCITY_DEFAULT;

        private Character _character;

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
#if UNITY_EDITOR
            _character.MaxVelocity = _maxVelocity;
            _character.Velocity = _velocity;
#endif
            base.Update();
            transform.position = _character.Position;
        }
    }
}