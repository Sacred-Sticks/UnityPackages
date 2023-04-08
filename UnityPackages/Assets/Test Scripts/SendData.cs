using Essentials.Events;
using UnityEngine;

public class SendData : MonoBehaviour
{
    [SerializeField] private Movement mover;

    private void Awake()
    {
        EventManager.Trigger(new EventCall("SendMover", this, mover));
    }
}