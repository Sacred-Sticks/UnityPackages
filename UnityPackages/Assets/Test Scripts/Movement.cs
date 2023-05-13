using System;
using Kickstarter.Groups;
using Kickstarter.Inputs;
using Kickstarter.References;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    [SerializeField] private Vector2Input input;
    [SerializeField] private FloatReference speedVar;
    
    private Rigidbody body;
    private float speed
    {
        get
        {
            return speedVar.Value;
        }
    }
    private Vector3 velocity;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        input.ValueChanged += OnInputChanged;
    }

    private void OnInputChanged()
    {
        var direction = new Vector3(input.Value.x, 0, input.Value.y);
        body.velocity = direction * speed;
    }
}