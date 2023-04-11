using System;
using System.Collections.Generic;
using UnityEngine.InputSystem;

namespace Essentials.Events
{
    public static class EventManager
    {
        public static Dictionary<Type, Action<GameEvent>> Events = new Dictionary<Type, Action<GameEvent>>();

        public static void AddListener<T>(Action<GameEvent> listener) where T : GameEvent
        {
            if (!Events.ContainsKey(typeof(T)))
                Events.Add(typeof(T), null);

            Events[typeof(T)] += listener;
        }

        public static void RemoveListener<T>(Action<GameEvent> listener) where T : GameEvent
        {
            if (!Events.ContainsKey(typeof(T)))
                return;
            Events[typeof(T)] -= listener;
        }

        public static void Trigger<T>(T arguments) where T : GameEvent
        {
            if (!Events.TryGetValue(typeof(T), out var listeners))
                return;
            listeners?.Invoke(arguments);
        }
    }

    public abstract class GameEvent
    {
        protected GameEvent(object sender)
        {
            Sender = sender;
        }

        public object Sender { get; }
        
    }
}
