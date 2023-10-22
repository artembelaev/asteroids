using System;

namespace AsteroidGame
{
    public class Entity
    {
        public bool IsKilled { get; private set; }

        public event Action<Entity> OnKill;

        public bool IsBlink
        {
            get => _isBlink;
            set
            {
                if (value == _isBlink)
                    return;
                _isBlink = value;
                OnBlinkChanged?.Invoke(this, value);
            }
        }

        private bool _isBlink;

        public event Action<Entity, bool> OnBlinkChanged;

        public virtual void Tick(float dt)
        {
        }

        public virtual void Kill()
        {
            if (IsKilled)
                return;

            IsKilled = true;
            OnKill?.Invoke(this);
        }

    }
}