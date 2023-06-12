using System;
using System.Collections.Generic;

namespace Kickstarter.Events
{
    public static class EventManager
    {
        private static readonly Dictionary<string, Delegate> keyedEvents = new Dictionary<string, Delegate>();
        private static readonly Dictionary<Type, Delegate> unkeyedEvents = new Dictionary<Type, Delegate>();

        public static void AddListener<T>(string key, Action<T> listener)
        {
            if (!keyedEvents.ContainsKey(key))
                keyedEvents[key] = null;

            keyedEvents[key] = (keyedEvents[key] as Action<T>) + listener;
        }

        public static void RemoveListener<T>(string key, Action<T> listener)
        {
            if (keyedEvents.ContainsKey(key))
                keyedEvents[key] = (keyedEvents[key] as Action<T>) - listener;
        }

        public static void Trigger<T>(string key, T arguments)
        {
            if (!keyedEvents.ContainsKey(key))
                return;

            (keyedEvents[key] as Action<T>)?.Invoke(arguments);
        }

        public static void AddListener<T>(Action<T> listener)
        {
            var key = typeof(T);
            if (!unkeyedEvents.ContainsKey(key))
                unkeyedEvents[key] = null;

            unkeyedEvents[key] = (unkeyedEvents[key] as Action<T>) + listener;
        }

        public static void RemoveListener<T>(Action<T> listener)
        {
            var key = typeof(T);
            if (unkeyedEvents.ContainsKey(key))
                unkeyedEvents[key] = (unkeyedEvents[key] as Action<T>) - listener;
        }

        public static void Trigger<T>(T parameters)
        {
            var key = typeof(T);
            if (!unkeyedEvents.ContainsKey(key))
                return;
            
            (unkeyedEvents[key] as Action<T>)?.Invoke(parameters);
        }
    }
}
