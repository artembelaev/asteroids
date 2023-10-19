using System;
using JetBrains.Annotations;
using MVC;
using UnityEngine;

namespace AsteroidGame
{
    public class AnimatedSpaceshipView : ViewBehaviour
    {
        [SerializeField] private Animator _shipAnimator;
        [SerializeField] private Animator _shipEngineAnimator;

        [CanBeNull] private Spaceship _ship;

        private static readonly int PARAM_KILLED = Animator.StringToHash("killed");
        private static readonly int PARAM_ENGINE_ENABLED = Animator.StringToHash("engine_enabled");

        private void Start()
        {
            _ship = Model as Spaceship;
        }

        private void Update()
        {
            if (_ship == null)
                return;

            if (_shipAnimator != null)
                _shipAnimator.SetBool(PARAM_KILLED, _ship.IsKilled);

            if (_shipEngineAnimator != null)
                _shipAnimator.SetBool(PARAM_ENGINE_ENABLED, _ship.EngineEnabled);
        }
    }
}