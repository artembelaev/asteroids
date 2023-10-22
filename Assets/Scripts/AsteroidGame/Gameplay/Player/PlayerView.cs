using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

namespace AsteroidGame
{
    public class PlayerView : AnimatedSpaceshipView
    {
        [SerializeField] private GameObject _visual;
        [SerializeField] private float _blinkTime = 0.25f;

        [CanBeNull] private Player _player;
        private Coroutine _blinkCoroutine;

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
            }
        }

        private void OnDestroy()
        {
            if (_player != null)
            {
                _player.OnBlinkChanged -= OnBlinkChanged;
                _player.OnRespawn -= OnRespawn;
            }
        }

        private void OnRespawn(Entity entity, Vector2 _)
        {
            Visible = true;
            if (entity.IsBlink) // restore blink
                OnBlinkChanged(entity, true);
        }


        private void OnBlinkChanged(Entity _, bool blink)
        {
            if (_player == null)
                return;

            if (blink && _player.NeedRespawn)
            {
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