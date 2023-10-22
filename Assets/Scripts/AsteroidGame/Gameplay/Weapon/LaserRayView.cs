using UnityEngine;

namespace AsteroidGame
{
    public class LaserRayView : MonoBehaviour
    {
        [SerializeField] private Transform _rayTransform;

        private float _distance;

        public float Distance
        {
            get => _distance;
            set
            {
                _distance = value;
                _rayTransform.localScale = new Vector3(1f, value);
            }
        }
    }
}