using JetBrains.Annotations;
using UnityEngine;

namespace AsteroidGame
{
    [RequireComponent(typeof(CharacterController))]
    public class CharacterKillOutsideScreen : MonoBehaviour, IPositionModifier
    {
        [CanBeNull] private Character _character;

        private void Start()
        {
            var controller = GetComponent<CharacterController>();
            if (controller == null)
                return;
            _character = controller.GetModel<Character>();

            _character.AddPositionModifier(this);
        }

        private void OnDestroy()
        {
            _character?.AddPositionModifier(this);
        }

        public Vector2 Modify(Vector2 position)
        {
            Vector2 pos = _character.Position;

            Camera mainCamera = Camera.main;
            if (mainCamera == null || !enabled)
                return pos;

            float halfScreenHeight = mainCamera.orthographicSize;
            float halfScreenWidth = halfScreenHeight * mainCamera.aspect;

            bool needKill = pos.x > halfScreenWidth ||
                            pos.x < -halfScreenWidth  ||
                            pos.y > halfScreenHeight  ||
                            pos.y < -halfScreenHeight;


            return pos;
        }
    }
}