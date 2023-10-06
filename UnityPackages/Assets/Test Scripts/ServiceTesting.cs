using System;
using Kickstarter.Events;
using UnityEngine;
using IServiceProvider = Kickstarter.Events.IServiceProvider;

public class ServiceTesting : MonoBehaviour, IServiceProvider
{
    [SerializeField] private Service debugging;
    
    private void Awake()
    {
        debugging.Event += ImplementService;
    }

    private void Start()
    {
        debugging.Trigger(new DebugArgs("DEBUGGING"));
    }

    public void ImplementService(EventArgs args)
    {
        if (args is not DebugArgs)
            return;
        var arguments = (DebugArgs)args;
        Debug.Log(arguments.info);
    }

    public class DebugArgs : EventArgs
    {
        public DebugArgs(string info)
        {
            this.info = info;
        }

        public string info { get; private set; }
    }
}
