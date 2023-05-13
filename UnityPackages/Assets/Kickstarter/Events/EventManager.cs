using System;
using System.Collections.Generic;

namespace Kickstarter.Events
{
    public static class EventManager
    {
        public static Dictionary<string, Action<GameEvent>> Events = new Dictionary<string, Action<GameEvent>>();

        public static void AddListener(string key, Action<GameEvent> listener)
        {
            if (!Events.ContainsKey(key))
                Events.Add(key, null);

            Events[key] += listener;
        }

        public static void RemoveListener(string key, Action<GameEvent> listener)
        {
            if (!Events.ContainsKey(key))
                return;
            Events[key] -= listener;
        }

        public static void Trigger(string key, GameEvent arguments)
        {
            if (!Events.TryGetValue(key, out var listeners))
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
