using System;
using UnityEngine;

namespace Essentials.Events
{
    [CreateAssetMenu(fileName = "Event Chain", menuName = "Events/Linker")]
    public class EventChain : ScriptableObject
    {
        public delegate void Chain(object sender, EventArgs e);

        public event Chain ChainedEvent;

        public void CallEvent(object sender, EventArgs e)
        {
            ChainedEvent?.Invoke(sender, e);
        }
    }
}
