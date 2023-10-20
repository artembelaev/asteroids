﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidGame
{
    public class Character : Entity
    {
        public const float MAX_VELOCITY_DEFAULT = 10f;

        public Vector2 Position { get; set; }
        public float Rotation { get; set; }

        public Vector2 Velocity
        {
            get => _velocity;
            set => _velocity =  Mathf.Clamp(value.magnitude, 0f, MaxVelocity) * value.normalized;
        }

        public float MaxVelocity { get; set; }
        public bool IsKilled { get; private set; }

        public event Action<Character> OnKill;

        private Vector2 _velocity;
        private HashSet<IPositionModifier> _modifiers = new ();

        public Character(Vector2 position = default, float rotation = 0f, Vector2 velocity = default, float maxVelocity = MAX_VELOCITY_DEFAULT)
        {
            Position = position;
            Rotation = rotation;
            MaxVelocity = maxVelocity;
            Velocity = velocity;
        }

        public override void Tick(float dt)
        {
            base.Tick(dt);
            UpdatePosition(dt);

            foreach (IPositionModifier modifier in _modifiers)
            {
                Position = modifier.Modify(Position);
            }
        }

        public void Kill()
        {
            if (IsKilled)
                return;

            IsKilled = true;
            OnKill?.Invoke(this);
        }

        public void AddPositionModifier(IPositionModifier modifier)
        {
            _modifiers.Add(modifier);
        }

        public void RemovePositionModifier(IPositionModifier modifier)
        {
            _modifiers.Remove(modifier);
        }

        protected virtual void UpdatePosition(float dt)
        {
            Position += Velocity * dt;
        }
    }
}