using Essentials.Events;
using UnityEngine;

public class SendData : MonoBehaviour
{
    [SerializeField] private Movement mover;
    private EventCall sendMovement;

    private void Awake()
    {
        sendMovement = new EventCall(this, mover);
        EventManager.Trigger(sendMovement);
    }
}