using System.Collections.Generic;
using AsteroidGame;
using UnityEngine;
using NotImplementedException = System.NotImplementedException;

namespace AsteroidGame
{
    public class AsteroidsFactory : MonoBehaviour, IAsteroidsFactory
    {
        [SerializeField] private CharacterControllerBehaviour _characterPrefab;
        [SerializeField] private Transform _parent;

        private List<CharacterControllerBehaviour> _created = new ();

        public Character Create(int level)
        {
            CharacterControllerBehaviour controller = Instantiate(_characterPrefab, _parent);
            return controller.GetModel<Character>();
        }

        public void ClearAll()
        {
            foreach (CharacterControllerBehaviour asteroidController in _created)
            {
                Destroy(asteroidController.gameObject);
            }
        }
    }
}