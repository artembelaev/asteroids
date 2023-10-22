﻿using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace AsteroidGame
{
    public class PlayerView : AnimatedSpaceshipView
    {
        [SerializeField] private GameObject _visual;
        [SerializeField] private float _blinkTime = 0.25f;
        [SerializeField] private Animator _engineAnimator;

        [CanBeNull] private Player _player;
        private Coroutine _blinkCoroutine;

        private static readonly int EnabledParam = Animator.StringToHash("enabled");

        private bool Visible
        {
            get => _visual.gameObject.activeSelf;
            set => _visual.gameObject.SetActive(value);
        }

        protected virtual void Start()
        {
            base.Start();
            _player = Model as Player;
            if (_player != null)
            {
                _player.OnBlinkChanged += OnBlinkChanged;
                _player.OnRespawn += OnRespawn;
                _player.OnKill += OnKill;
                _player.OnEngineEnabled += OnOnEngineEnabled;
            }
        }

        private void OnOnEngineEnabled(bool enable)
        {
            _engineAnimator.SetBool(EnabledParam, enable);
        }

        private void OnDestroy()
        {
            if (_player != null)
            {
                _player.OnBlinkChanged -= OnBlinkChanged;
                _player.OnRespawn -= OnRespawn;
                _player.OnKill -= OnKill;
                _player.OnEngineEnabled -= OnOnEngineEnabled;
            }
        }

        private void OnRespawn(Entity entity, Vector2 _)
        {
            Visible = true;
            if (entity.IsBlink) // restore blink
                OnBlinkChanged(entity, true);
        }

        private void OnKill(Entity _)
        {
            Visible = false; // TODO animation
        }

        private void OnBlinkChanged(Entity _, bool blink)
        {
            if (_player == null)
                return;

            if (blink && _player.NeedRespawn)
            {
                Visible = false; // TODO animation
                return;
            }

            if (blink && _blinkCoroutine == null)
            {
                _blinkCoroutine = StartCoroutine(BlinkCoroutine());
            }
            else if (!blink && _blinkCoroutine != null)
            {
                StopCoroutine(_blinkCoroutine);
                _blinkCoroutine = null;
                Visible = true;
            }
        }

        private IEnumerator BlinkCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(_blinkTime);
                Visible = !Visible;
            }
        }
    }
}