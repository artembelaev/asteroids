using UnityEngine;

namespace AsteroidGame
{
    public class AsteroidController : CharacterController
    {
        [SerializeField] private int _level;
        [SerializeField] private int _levelCount = 3;
        [SerializeField] private int _childCount = 2;
        [SerializeField] private float _childMaxAngleScatter = Asteroid.CHILD_MAX_ANGLE;

        private Asteroid _asteroid;
        private IAsteroidsFactory _asteroidsFactory;

        public void Construct(IAsteroidsFactory asteroidsFactory)
        {
            _asteroidsFactory = asteroidsFactory;
        }

        protected override object CreateModel()
        {
            return new Asteroid(_level, transform.position,
                transform.rotation.z, _velocity, _maxVelocity)
            {
                LevelCount = _levelCount,
                ChildCount = _childCount,
                ChildMaxAngleScatter = _childMaxAngleScatter,
            };
        }

        protected override void Awake()
        {
            base.Awake();
            _asteroid = GetModel<Asteroid>();
            _asteroid.OnCreateChild += OnCreateChild;
        }


        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.TryGetComponent(out CharacterController characterController) ||
                characterController.Character.IsKilled ||
                characterController.Character.IsBlink)
                return;

            characterController.Character.Kill();
            _asteroid.Kill();
        }

        private void OnCreateChild(Vector2 childVelocity)
        {
            Asteroid child = _asteroidsFactory.Create(_asteroid.Level + 1, _asteroid.Position);
            child.Velocity = childVelocity;
        }
    }
}