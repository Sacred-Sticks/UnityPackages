using System;
using System.Collections.Generic;

namespace Kickstarter.Events
{
    public static class EventManager
    {
        public static Dictionary<string, Action<object>> Events = new Dictionary<string, Action<object>>();

        public static void AddListener<T>(string key, Action<T> listener) where T : class
        {
            if (!Events.ContainsKey(key))
            {
                Events[key] = null;
            }

            Events[key] += listener as Action<object>;
        }

        public static void RemoveListener<T>(string key, Action<T> listener) where T : class
        {
            if (Events.ContainsKey(key))
                Events[key] -= listener as Action<object>;
        }

        public static void Trigger<T>(string key, T arguments) where T : class
        {
            if (!Events.ContainsKey(key))
                return;

            Events[key]?.Invoke(arguments);
        }
    }
}
