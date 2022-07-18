using UnityEngine;
using Zenject;

namespace Scorewarrior.Test.Models
{
    public interface IStateMachine
    {
        void ChangeTo(State state);
        void Update();
    }
    
    public class StateMachine : IStateMachine
    {
        private readonly DiContainer _container;

        private State _activeState;

        public StateMachine(DiContainer container)
        {
            _container = container;
        }

        public void ChangeTo(State state)
        {
            _container.Inject(state);
            
            _activeState?.OnExit();
            state.OnEnter();

            _activeState = state;
        }

        public void Update()
        {
            _activeState?.OnUpdate();
        }
    }
}