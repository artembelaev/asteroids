using JetBrains.Annotations;
using UnityEngine;

namespace AsteroidGame
{
    public class ProjectileController : MonoBehaviour
    {
        [CanBeNull] private Entity _projectile;

        private void Start()
        {
            var controller = GetComponent<EntityController>();
            if (controller == null)
                return;
            _projectile = controller.Entity;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!CanCollide(_projectile) ||
                !col.TryGetComponent(out CharacterController characterController))
                return;

            Character character = characterController.Character;

            if (!CanCollide(character))
                return;

            _projectile.Kill();
            character.Kill();

            bool CanCollide(Entity e)
            {
                return e != null && !e.IsKilled && !e.IsBlink;
            }
        }
    }
}