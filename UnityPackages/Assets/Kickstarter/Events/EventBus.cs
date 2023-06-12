using System;
using UnityEngine;

namespace Kickstarter.Events
{
    [CreateAssetMenu(fileName = "On Event", menuName = "Events/Event Bus")]
    public sealed class EventBus : ScriptableObject
    {
        public Action<EventArgs> Event;

        public void CallEvent(EventArgs parameters)
        {
            Event?.Invoke(parameters);
        }
    }
}
