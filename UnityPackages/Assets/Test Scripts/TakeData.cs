using System;
using Essentials.Events;
using UnityEngine;

public class TakeData : MonoBehaviour
{
    [SerializeField] private EventChain eventReceiver;
    [SerializeField] private Movement mover;
    
    private void Awake()
    {
        eventReceiver.ChainedEvent += OnChainedEventCalled;
    }

    private void OnChainedEventCalled(object sender, EventArgs e)
    {
        if (e is not DataArgs dataArgs) 
            return;
        if (dataArgs.MonoBehaviours[0] is not Movement movement) return;
        mover = movement;
    }
}