using Essentials.Events;
using UnityEngine;

public class TakeData : MonoBehaviour
{
    [SerializeField] private string eventKey;
    [SerializeField] private Movement mover;

    private EventCall takeMovement;

    private void OnEnable()
    {
        EventManager.AddListener(eventKey, OnEventCalled);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener(eventKey, OnEventCalled);
    }

    private void OnEventCalled(GameEvent e)
    {
        if (e is not EventCall args)
            return;
        mover = args.movement;
    }
}

public sealed class EventCall : GameEvent
{
    public EventCall(object sender, Movement movement) : base(sender)
    {
        this.movement = movement;
    }

    public Movement movement { get; }
}
