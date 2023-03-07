using System.Collections.Generic;
using Essentials.Events;
using UnityEngine;
public class SendData : MonoBehaviour
{
    [SerializeField] private EventChain eventTarget;
    [SerializeField] private List<MonoBehaviour> dataSet;

    private void Start()
    {
        var e = new DataArgs(dataset: dataSet);
        eventTarget.CallEvent(this, e);
    }
}