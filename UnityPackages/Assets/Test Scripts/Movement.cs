using Kickstarter.Identification;
using Kickstarter.Inputs;
using Kickstarter.References;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Player))]
public class Movement : MonoBehaviour
{
    [SerializeField] private Vector2Input movementInput;
    [SerializeField] private FloatInput jumpInput;
    [SerializeField] private float inputTolerance;
    [Space(20)]
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
    }
    
    private void Start()
    {
        var player = GetComponent<Player>();
        movementInput.SubscribeToInputAction(OnMovementInputChange, player.PlayerID);
        jumpInput.SubscribeToInputAction(OnJumpInputChange, player.PlayerID);
        jumpSpeed = Mathf.Sqrt(2 * -Physics.gravity.y * JumpHeight);
    }

    private void OnJumpInputChange(float input)
    {
        if (input < inputTolerance)
            return;
        body.AddForce(Vector3.up * jumpSpeed, ForceMode.VelocityChange);
    }

    private void OnMovementInputChange(Vector2 input)
    {
        var direction = input.x * transform.right + input.y * transform.forward;
        velocity = direction * Speed;
    }

    private void FixedUpdate()
    {
        body.velocity = velocity + body.velocity.y * Vector3.up;
    }
}