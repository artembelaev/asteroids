using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace AsteroidGame
{

    public class AsteroidsFactory : MonoBehaviour, IAsteroidsFactory
    {
        [SerializeField] private List<AsteroidController> _prefabs;
        [SerializeField] private Transform _parent;

        private Dictionary<Asteroid, AsteroidController> _created = new ();

        public Asteroid Create(int level, Vector2 position)
        {
            Assert.IsTrue(level < _prefabs.Count);

            AsteroidController controller = Instantiate(_prefabs[level], position, Quaternion.identity, _parent);

            controller.Construct(this);
            var asteroid = controller.GetModel<Asteroid>();
            asteroid.OnKill += OnAsteroidKill;
            _created[asteroid] = controller;
            return asteroid;
        }

        private void OnAsteroidKill(Entity entity)
        {
            if (entity is Asteroid asteroid)
                Clear(asteroid);
        }

        private void Clear(Asteroid asteroid)
        {
            if (_created.Remove(asteroid, out AsteroidController controller))
                Destroy(controller.gameObject);
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