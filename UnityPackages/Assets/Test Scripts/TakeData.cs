using Kickstarter.Events;
using UnityEngine;

public class TakeData : MonoBehaviour
{
    [SerializeField] private Movement mover;

    private EventCall takeMovement;

    private void OnEnable()
    {
        EventManager.AddListener<EventCall>("Movement", OnEventCalled);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener<EventCall>("Movement", OnEventCalled);
    }

    private void OnEventCalled(EventCall args)
    {
        mover = args.movement;
    }
}

public sealed class EventCall
{
    public EventCall(Movement movement)
    {
        this.movement = movement;
    }

    public Movement movement { get; }
}
