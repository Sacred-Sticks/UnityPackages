using System;
using System.Collections.Generic;

namespace Kickstarter.StateControllers
{
    public class StateMachine<T> where T : Enum
    {
        private StateMachine(T initialState)
        {
            State = initialState;
        }

        public enum StateChange
        {
            Begin,
            End,
        }

        private T state;
        public T State
        {
            get => state;
            set
            {
                if (!stateTransitions[State].Contains(value))
                    return;
                onStateEnd[state]();
                state = value;
                onStateBegin[state]();
            }
        }

        private Dictionary<T, List<T>> stateTransitions;
        private Dictionary<T, Action> onStateBegin;
        private Dictionary<T, Action> onStateEnd;

        private void AddTransition(T baseState, T newState)
        {
            if (!stateTransitions.ContainsKey(baseState))
                stateTransitions.Add(baseState, new List<T>());
            if (!stateTransitions[baseState].Contains(newState))
                stateTransitions[baseState].Add(newState);
        }

        public void SubscribeToStateChange(StateChange changeType, T state, Action subscription)
        {
            switch (changeType)
            {
                case StateChange.Begin:
                    onStateBegin[state] += subscription;
                    break;
                case StateChange.End:
                    onStateEnd[state] += subscription;
                    break;
            }
        }

        public void UnsubscribeToStateChange(StateChange changeType, T state, Action subscription)
        {
            switch (changeType)
            {
                case StateChange.Begin:
                    onStateBegin[state] -= subscription;
                    break;
                case StateChange.End:
                    onStateEnd[state] -= subscription;
                    break;
            }
        }

        public class Builder<TEnum> where TEnum : Enum
        {
            private TEnum initialState;
            private Dictionary<TEnum, List<TEnum>> transitions;
            
            private Builder(TEnum initialState)
            {
                this.initialState = initialState;
            }

            public Builder<TEnum> CreateStateMachine(TEnum initialState)
            {
                return new Builder<TEnum>(initialState);
            }

            public Builder<TEnum> WithTransition(TEnum fromState, TEnum toState)
            {
                if (!transitions.ContainsKey(fromState))
                    transitions.Add(fromState, new List<TEnum>());
                if (!transitions[fromState].Contains(toState))
                    transitions[fromState].Add(toState);
                return this;
            }

            public StateMachine<TEnum> Build()
            {
                var stateMachine = new StateMachine<TEnum>(initialState);
                foreach (var (fromState, toStates) in transitions)
                {
                    toStates.ForEach(toState => stateMachine.AddTransition(fromState, toState));
                }
                return stateMachine;
            }
        }
    }
}
