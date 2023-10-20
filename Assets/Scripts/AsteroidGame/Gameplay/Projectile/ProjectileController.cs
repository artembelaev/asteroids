using JetBrains.Annotations;
using UnityEngine;

namespace AsteroidGame
{
    public class ProjectileController : MonoBehaviour
    {
        [CanBeNull] private Character _projectile;

        private void Start()
        {
            var controller = GetComponent<CharacterController>();
            if (controller == null)
                return;
            _projectile = controller.Character;
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

            bool CanCollide(Character ch)
            {
                return ch != null && !ch.IsKilled && !ch.IsBlink;
            }
        }
    }
}