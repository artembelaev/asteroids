using System;
using UnityEngine;

namespace AsteroidGame
{
    public class LaserRayController : MonoBehaviour
    {
        [SerializeField] private LayerMask _raycastMask;
        [SerializeField] private LaserRayView _rayView;
        [SerializeField] private float _duration = 0.5f;
        [SerializeField] private float _maxDistance = 50f;
        [SerializeField] private bool _hitOnce = true;

        private float _timeToDisable;
        private RaycastHit2D[] hits = new RaycastHit2D[1];
        private bool _canHit = true;

        private void OnEnable()
        {
            _timeToDisable = _duration;
        }

        private void Update()
        {
            _timeToDisable -= Time.deltaTime;
            if (_timeToDisable <= 0f)
                Destroy(gameObject);
        }

        private void FixedUpdate()
        {
            Raycast(transform.position, transform.up);
        }

        private void Raycast(Vector3 position, Vector3 direction)
        {
            int hitsCount = Physics2D.RaycastNonAlloc(position, direction, hits, _maxDistance, _raycastMask);

#if UNITY_EDITOR
            Debug.DrawRay(position, direction, Color.green);
#endif

            if (hitsCount == 0 || hits.Length == 0)
            {
                _rayView.Distance = _maxDistance;
                return;
            }

            float distance = hits[0].distance;
            _rayView.Distance = distance;

            if (_canHit)
            {
                var entityController = hits[0].collider.GetComponent<EntityController>();
                if (entityController != null)
                    entityController.Entity.Kill();

                if (_hitOnce)
                    _canHit = false;
            }
        }
    }
}