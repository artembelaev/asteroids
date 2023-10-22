using UnityEngine;
using MVC;

namespace AsteroidGame
{
    public abstract class EntityController : ControllerBehaviour
    {
        private Entity _entity;

        public Entity Entity => _entity;

        protected override object CreateModel()
        {
            return new Entity();
        }

        protected override void Awake()
        {
            base.Awake();
            _entity = GetModel<Entity>();
        }

        protected virtual void Update()
        {
            _entity.Tick(Time.deltaTime);
        }
    }
}