using Essentials.Events;
using UnityEngine;

public class TakeData : MonoBehaviour
{
    [SerializeField] private Movement mover;

    private EventCall takeMovement;

    private void Awake()
    {
        EventManager.AddListener<EventCall>(OnEventCalled);
    }

    private void OnDestroy()
    {
        EventManager.RemoveListener<EventCall>(OnEventCalled);
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
