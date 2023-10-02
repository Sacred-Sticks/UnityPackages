using System;
using Kickstarter.Events;
using UnityEngine;

[RequireComponent(typeof(ServiceProvider))]
public class ServiceTesting : MonoBehaviour, IServiceLocator
{
    [SerializeField] private Service debugging;

    private void Awake()
    {
        var provider = GetComponent<ServiceProvider>();
        provider.Service.Event += ImplementService;
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
