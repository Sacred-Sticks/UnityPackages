using System;
using System.Collections.Generic;
using UnityEngine;

namespace Essentials.Events
{
    public static class EventManager
    {
        public static Dictionary<string, Action<GameEvent>> Events = new Dictionary<string, Action<GameEvent>>();

        public static void AddListener(string key, Action<GameEvent> listener)
        {
            if (Events.ContainsKey(key))
                Events[key] += listener;
            else
                Events[key] = listener;
        }

        public static void RemoveListener(string key, Action<GameEvent> listener)
        {
            if (!Events.ContainsKey(key))
                return;
            Events[key] -= listener;
        }

        public static void Trigger<TEventArgs>(TEventArgs arguments) where TEventArgs : GameEvent
        {
            string key = arguments.Key;
            if (Events.TryGetValue(key, out var listeners))
            {
                listeners?.Invoke(arguments);
            }
            else
                Debug.LogWarning($"{key} Not Found: Typo in {key} Trigger Call or {key} Subscription.");
        }
    }

    public abstract class GameEvent
    {
        protected GameEvent(string key, object sender)
        {
            Sender = sender;
            Key = key;
        }

        public object Sender { get; }
        public string Key { get; }
        
    }
}
