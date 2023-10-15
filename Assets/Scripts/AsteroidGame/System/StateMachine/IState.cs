using System;

namespace AsteroidGame.System.States
{
    public interface IState<TStateId> where TStateId : struct, Enum
    {
        TStateId Id { get; }
        IStateMachine<TStateId> StateMachine { get; set; }
        void OnEnter();
        void OnExit();
    }
}