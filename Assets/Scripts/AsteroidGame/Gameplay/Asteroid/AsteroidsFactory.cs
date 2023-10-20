using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AsteroidGame
{

    public class AsteroidsFactory : MonoBehaviour, IAsteroidsFactory
    {
        [SerializeField] private List<AsteroidController> _prefabs;
        [SerializeField] private Transform _parent;

        private Dictionary<int, AsteroidController> _prefabsDict;
        private Dictionary<Asteroid, AsteroidController> _created = new ();

        private Dictionary<int, AsteroidController> PrefabsDict =>
            _prefabsDict ??= _prefabs.Select((item, index) => new { Index = index, Item = item })
                .ToDictionary(pair => pair.Index, pair => pair.Item);

        public Asteroid Create(int level, Vector2 position)
        {
            if (!PrefabsDict.TryGetValue(level, out AsteroidController prefab))
                return null;

            AsteroidController controller = Instantiate(prefab, position, Quaternion.identity, _parent);

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