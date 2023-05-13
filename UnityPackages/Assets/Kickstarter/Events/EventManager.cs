using System;
using System.Collections.Generic;

namespace Kickstarter.Events
{
    public static class EventManager
    {
        public static Dictionary<Type, List<Action<object>>> Events = new Dictionary<Type, List<Action<object>>>();

        public static void AddListener<T>(Action<T> listener) where T : class
        {
            var key = typeof(T);
        
            if (!Events.ContainsKey(key))
            {
                Events[key] = new List<Action<object>>();
            }
        
            Events[key].Add(data => listener(data as T));
        }

        public static void RemoveListener<T>(Action<T> listener) where T : class
        {
            var key = typeof(T);

            if (Events.ContainsKey(key))
            {
                Events[key].Remove(data => listener(data as T));
            }
        }

        public static void Trigger<T>(T arguments) where T : class
        {
            var key = typeof(T);

            if (!Events.ContainsKey(key))
                return;
            
            foreach (var action in Events[key])
            {
                action(arguments);
            }
        }
    }
}
