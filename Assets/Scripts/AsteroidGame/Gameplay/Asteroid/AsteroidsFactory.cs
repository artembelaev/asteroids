using System.Collections.Generic;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace AsteroidGame
{
    public class AsteroidsFactory : MonoBehaviour, IAsteroidsFactory
    {
        [SerializeField] private AsteroidController _asteroidPrefab;
        [SerializeField] private Transform _parent;

        private Dictionary<Asteroid, AsteroidController> _created = new ();

        public Asteroid Create(int level, Vector2 position)
        {
            AsteroidController controller = Instantiate(_asteroidPrefab,
                position, Quaternion.identity, _parent);
            controller.Construct(this);
            var asteroid = controller.GetModel<Asteroid>();
            asteroid.OnKill += OnAsteroidKill;
            _created[asteroid] = controller;
            return asteroid;
        }

        private void OnAsteroidKill(Character character)
        {
            if (character is Asteroid asteroid)
                Clear(asteroid);
        }

        private void Clear(Asteroid asteroid)
        {
            if (_created.Remove(asteroid, out AsteroidController controller))
                Destroy(controller);
        }

        public void ClearAll()
        {
            foreach (var pair in _created)
            {
                AsteroidController controller = pair.Value;
                Destroy(controller.gameObject);
            }
            _created.Clear();
        }
    }
}