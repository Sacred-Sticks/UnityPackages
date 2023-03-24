using System;
using UnityEngine;

namespace Essentials.Events
{
    [CreateAssetMenu(fileName = "Event Chain", menuName = "Events/Linker")]
    public class EventBus : ScriptableObject
    {
        public delegate void EventHandler(object sender, EventArgs e);

        public event EventHandler Event;

        public void CallEvent(object sender, EventArgs e)
        {
            Event?.Invoke(sender, e);
        }
    }
}
