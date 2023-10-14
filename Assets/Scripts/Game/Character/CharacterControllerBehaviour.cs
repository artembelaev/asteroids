using UnityEngine;

namespace Game
{
    public class CharacterControllerBehaviour : EntityControllerBehaviour
    {
        [SerializeField] private Vector2 _velocity;
        [SerializeField] private float _maxVelocity = Character.MAX_VELOCITY_DEFAULT;

        protected override object CreateModel()
        {
            return new Character(transform.position, transform.rotation.z, _velocity, _maxVelocity);
        }
    }
}