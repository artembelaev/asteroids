using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace AsteroidGame.System.States
{
    public class WrongStateException : Exception
    {
        public WrongStateException(string message) : base(message)
        {
        }
    }

    public abstract class StateMachine<TStateId> : IStateMachine<TStateId> where TStateId : struct, Enum
    {
        private readonly Dictionary<TStateId, IState<TStateId>> _states = new Dictionary<TStateId, IState<TStateId>>();

        [CanBeNull] public IState<TStateId> ActiveState { get; private set; }

        public void AddState(IState<TStateId> state)
        {
            state.StateMachine = this;
            _states.Add(state.Id, state);
        }

        public void Enter(TStateId stateId)
        {
            if (_states.TryGetValue(stateId, out var state))
                TransitTo(state);
            else
                throw new WrongStateException($"Unknown state: {stateId}");
        }

        private void TransitTo(IState<TStateId> state)
        {
            ActiveState?.OnExit();
            ActiveState = state;
            state.OnEnter();
        }
    }
}