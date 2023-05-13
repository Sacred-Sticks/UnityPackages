using Kickstarter.Events;
using UnityEngine;

public class SendData : MonoBehaviour
{
    [SerializeField] private Movement mover;
    private EventCall sendMovement;

    private void Start()
    {
        EventManager.Trigger(new EventCall(mover));
    }
}