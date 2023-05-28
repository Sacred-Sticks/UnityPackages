using System;
using System.Collections.Generic;

namespace Kickstarter.Events
{
    public static class EventManager
    {
        public static Dictionary<string, Delegate> Events = new Dictionary<string, Delegate>();

        public static void AddListener<T>(string key, Action<T> listener) where T : class
        {
            if (!Events.ContainsKey(key))
            {
                Events[key] = null;
            }

            Events[key] = (Events[key] as Action<T>) + listener;
        }

        public static void RemoveListener<T>(string key, Action<T> listener) where T : class
        {
            if (Events.ContainsKey(key))
                Events[key] = (Events[key] as Action<T>) - listener;
        }

        public static void Trigger<T>(string key, T arguments) where T : class
        {
            if (!Events.ContainsKey(key))
                return;

            (Events[key] as Action<T>)?.Invoke(arguments);
        }
    }
}
