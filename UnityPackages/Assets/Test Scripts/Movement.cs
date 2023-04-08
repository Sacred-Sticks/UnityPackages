using System;
using Essentials.Groups;
using Essentials.References;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    [SerializeField] private BidirectionalFloat inputs;
    [SerializeField] private FloatReference speedVar;
    
    private Rigidbody body;
    private float speed;
    private Vector3 velocity;

    private void Awake()
    {
        inputs.Up.ValueChanged += OnMovementValueChanged;
        inputs.Down.ValueChanged += OnMovementValueChanged;
        inputs.Left.ValueChanged += OnMovementValueChanged;
        inputs.Right.ValueChanged += OnMovementValueChanged;
        body = GetComponent<Rigidbody>();
    }

    private void OnMovementValueChanged(object sender, EventArgs e)
    {
        velocity = inputs.X * transform.right + inputs.Y * transform.forward;
        speed = speedVar.Value;
        velocity = velocity.normalized * speed;
        body.velocity = velocity;
    }
}