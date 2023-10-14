﻿using UnityEngine;
using MVC;

namespace Game
{
    public abstract class EntityControllerBehaviour : ControllerBehaviour
    {
        private Entity _entity;
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