using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DemoKeyboard : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private BidirectionalReferenceEnum movementEnums;
    [Space]
    [SerializeField] private float speed;

    private Rigidbody body;
    private Vector2 inputs;
    private Vector3 velocity;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        // Subscribe to events in inputManager sciptable object
        inputManager.OnInputPressedEvent += RecievePlayerInputPressed;
        inputManager.OnInputReleasedEvent += RecievePlayerInputReleased;
    }

    private void RecievePlayerInputPressed(object sender, PlayerInputArguments e)
    {
        // Makeshift Switch Statement because SO's cannot be declared as const, therefore cannot be a case
        if (e.InputType == movementEnums.Up)
        {
            inputs.y = inputs.y == 0 ? 1 : 0;
            goto InputReceived;
        }
        if (e.InputType == movementEnums.Down)
        {
            inputs.y = inputs.y == 0 ? -1 : 0;
            goto InputReceived;
        }
        if (e.InputType == movementEnums.Left)
        {
            inputs.x = inputs.x == 0 ? -1 : 0;
            goto InputReceived;
        }
        if (e.InputType == movementEnums.Right)
        {
            inputs.x = inputs.x == 0 ? 1 : 0;
            goto InputReceived;
        }
        // Default Case Goes Here
        return;
    InputReceived:
        // Anything After Switch Statement still needed after modifying values goes here
        velocity = inputs.x * transform.right + inputs.y * transform.forward;
        velocity = velocity.normalized * speed;
    }

    private void RecievePlayerInputReleased(object sender, PlayerInputArguments e)
    {
        // Makeshift Switch Statement because SO's cannot be declared as const, therefore cannot be a case
        if (e.InputType == movementEnums.Up)
        {
            inputs.y = inputs.y == 0 ? -1 : 0;
            goto InputReceived;
        }
        if (e.InputType == movementEnums.Down)
        {
            inputs.y = inputs.y == 0 ? 1 : 0;
            goto InputReceived;
        }
        if (e.InputType == movementEnums.Left)
        {
            inputs.x = inputs.x == 0 ? 1 : 0;
            goto InputReceived;
        }
        if (e.InputType == movementEnums.Right)
        {
            inputs.x = inputs.x == 0 ? -1 : 0;
            goto InputReceived;
        }
        // Default Case Goes Here
        return;
    InputReceived:
        // Anything After Switch Statement still needed after modifying values goes here
        velocity = inputs.x * transform.right + inputs.y * transform.forward;
        velocity = velocity.normalized * speed;
    }

    private void FixedUpdate()
    {
        body.velocity = velocity;
    }
}
