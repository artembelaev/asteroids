using System;

namespace AsteroidGame.System.States
{
    public class State<TStateId> : IState<TStateId> where TStateId : struct, Enum
    {
        public State(TStateId id)
        {
            Id = id;
        }

        public TStateId Id { get; }

        public IStateMachine<TStateId> StateMachine { get; set; }

        public virtual void OnEnter()
        {

        }

        public virtual void OnExit()
        {

        }
    }
}