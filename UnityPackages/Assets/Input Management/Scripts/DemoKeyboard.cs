using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DemoKeyboard : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private BidirectionalReferenceEnum movementEnums;
    [Space(10)]
    [Header("Inputs")]
    [SerializeField] private FloatReference Forward;
    [SerializeField] private FloatReference Left;
    [SerializeField] private FloatReference Backward;
    [SerializeField] private FloatReference Right;
    [SerializeField] private FloatReference Speed;
    [Space(10)]
    [Header("Outputs")]
    [SerializeField] private Vector3Reference Velocity;


    private Rigidbody body;
    private Vector2 inputs;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        SetInputs();
        SetVelocity();
        body.velocity = Velocity.Value;
    }

    private void SetInputs()
    {
        inputs.x = Right.Value - Left.Value;
        inputs.y = Forward.Value - Backward.Value;
    }

    private void SetVelocity()
    {
        Velocity.Value = (inputs.x * transform.right + inputs.y * transform.forward).normalized;
        Velocity.Value *= Speed.Value;
        Velocity.Value += body.velocity.y * Vector3.up;
    }
}
