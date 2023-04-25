using Essentials.Events;
using UnityEngine;

public class SendData : MonoBehaviour
{
    [SerializeField] private string eventKey;
    [SerializeField] private Movement mover;
    private EventCall sendMovement;

    private void Start()
    {
        EventManager.Trigger(eventKey, new EventCall(this, mover));
    }
}