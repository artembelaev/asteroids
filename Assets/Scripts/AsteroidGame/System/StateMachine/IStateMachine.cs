using System;

namespace AsteroidGame.System.States
{
    public interface IStateMachine<TStateId> where TStateId : struct, Enum
    {
        IState<TStateId> ActiveState { get; }
        void AddState(IState<TStateId> state);
        void Enter(TStateId stateId);

    }
}