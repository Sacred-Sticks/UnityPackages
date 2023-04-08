using Essentials.Events;
using UnityEngine;

public class TakeData : MonoBehaviour
{
    [SerializeField] private Movement mover;

    private const string eventKey = "SendMover";

    private void Awake()
    {
        EventManager.AddListener(eventKey, OnEventCalled);
    }

    private void OnDestroy()
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
    public EventCall(string key, object sender, Movement movement) : base(key, sender)
    {
        this.movement = movement;
    }

    public Movement movement { get; }
}
