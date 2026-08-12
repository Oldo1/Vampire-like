using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.GameStates
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IGameState> _states;
        public IGameState CurrentState { get; private set; }

        public GameStateMachine(IGameState[] states)
        {
            _states = states.ToDictionary(state => state.GetType());
        }

        public void SwitchState<TState>() where TState : IGameState
        {
            if (!_states.TryGetValue(typeof(TState), out var nextState))
                throw new InvalidOperationException($"State {typeof(TState).Name} is not registered");

            CurrentState?.Exit();
            CurrentState = nextState;
            CurrentState.Enter();
        }

        public void ExitCurrentState()
        {
            CurrentState?.Exit();
            CurrentState = null;
        }
    }
}
