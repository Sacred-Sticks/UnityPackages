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

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        var velocity = inputs.X * transform.right + inputs.Y * transform.forward;
        speed = speedVar.Value;
        velocity = velocity.normalized * speed;
        body.velocity = velocity;
    }
}
