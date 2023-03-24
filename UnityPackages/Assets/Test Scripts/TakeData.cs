using System;
using Essentials.Events;
using UnityEngine;

public class TakeData : MonoBehaviour
{
    [SerializeField] private EventBus eventReceiver;
    [SerializeField] private Movement mover;
    
    private void Awake()
    {
        eventReceiver.Event += OnEventCalled;
    }

    private void OnEventCalled(object sender, EventArgs e)
    {
        if (e is not DataArgs dataArgs) 
            return;
        if (dataArgs.MonoBehaviours[0] is not Movement movement) return;
        mover = movement;
    }
}