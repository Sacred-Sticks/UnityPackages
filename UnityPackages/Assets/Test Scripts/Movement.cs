using Kickstarter.Inputs;
using Kickstarter.References;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    [SerializeField] private Vector2Input movementInput;
    [SerializeField] private FloatInput jumpInput;
    [SerializeField] private float inputTolerance;
    [Space]
    [SerializeField] private FloatReference speed;
    [SerializeField] private FloatReference jumpHeight;
    
    private float Speed
    {
        get
        {
            return speed.Value;
        }
    }
    private float JumpHeight
    {
        get
        {
            return jumpHeight.Value;
        }
    }
    
    private Rigidbody body;
    private Vector3 velocity;
    private float jumpSpeed;
    
    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        movementInput.ValueChanged += OnMovementInputChanged;
        jumpInput.ValueChanged += JumpInputValueChanged;
    }

    private void Start()
    {
        jumpSpeed = Mathf.Sqrt(2 * -Physics.gravity.y * JumpHeight);
    }

    private void JumpInputValueChanged()
    {
        if (jumpInput.Value < inputTolerance)
            return;
        body.AddForce(Vector3.up * jumpSpeed, ForceMode.VelocityChange);
    }

    private void OnMovementInputChanged()
    {
        var direction = movementInput.Value.x * transform.right + movementInput.Value.y * transform.forward;
        velocity = direction * Speed;
    }

    private void FixedUpdate()
    {
        body.velocity = velocity + body.velocity.y * Vector3.up;
    }
}