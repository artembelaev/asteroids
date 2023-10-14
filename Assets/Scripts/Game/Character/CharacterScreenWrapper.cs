using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(CharacterControllerBehaviour))]
    public class CharacterScreenWrapper : MonoBehaviour, IPositionModifier
    {
        [CanBeNull] private Character _character;

        private void Start()
        {
            var controller = GetComponent<CharacterControllerBehaviour>();
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

            float screenHeight = mainCamera.orthographicSize * 2.0f;
            float screenWidth = screenHeight * mainCamera.aspect;

            if (pos.x > screenWidth / 2)
                pos.x = -screenWidth / 2;
            else if (pos.x < -screenWidth / 2)
                pos.x = screenWidth / 2;

            if (pos.y > screenHeight / 2)
                pos.y = -screenHeight / 2;
            else if (pos.y < -screenHeight / 2)
                pos.y = screenHeight / 2;

            return pos;
        }
    }
}