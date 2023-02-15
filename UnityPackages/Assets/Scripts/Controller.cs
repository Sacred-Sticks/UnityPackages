using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Controller : MonoBehaviour
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
        inputManager.OnInputPressedEvent += RecievePlayerInputPressed;
        inputManager.OnInputReleasedEvent += RecievePlayerInputReleased;
    }

    private void RecievePlayerInputPressed(object sender, PlayerInputArguments e)
    {
        if (e.InputType == movementEnums.Enum[Enums.Direction.Up])
        {
            inputs.y = inputs.y < 0 ? 0 : 1;
            goto InputReceived;
        }
        if (e.InputType == movementEnums.Enum[Enums.Direction.Down])
        {
            inputs.y = inputs.y > 0 ? 0 : 1;
            goto InputReceived;
        }
        if (e.InputType == movementEnums.Enum[Enums.Direction.Left])
        {
            inputs.x = inputs.x > 0 ? 0 : 1;
            goto InputReceived;
        }
        if (e.InputType == movementEnums.Enum[Enums.Direction.Right])
        {
            inputs.x = inputs.x < 0 ? 0 : 1;
            goto InputReceived;
        }

        InputReceived:
        velocity = inputs.x * transform.right + inputs.y * transform.forward;
        velocity = velocity.normalized * speed;
    }

    private void RecievePlayerInputReleased(object sender, PlayerInputArguments e)
    {
        if (e.InputType == movementEnums.Enum[Enums.Direction.Up])
        {
            inputs.y = inputs.y > 0 ? 0 : 1;
            goto InputReceived;
        }
        if (e.InputType == movementEnums.Enum[Enums.Direction.Down])
        {
            inputs.y = inputs.y < 0 ? 0 : 1;
            goto InputReceived;
        }
        if (e.InputType == movementEnums.Enum[Enums.Direction.Left])
        {
            inputs.x = inputs.x < 0 ? 0 : 1;
            goto InputReceived;
        }
        if (e.InputType == movementEnums.Enum[Enums.Direction.Right])
        {
            inputs.x = inputs.x > 0 ? 0 : 1;
            goto InputReceived;
        }

        InputReceived:
        velocity = inputs.x * transform.right + inputs.y * transform.forward;
        velocity = velocity.normalized * speed;
    }

    private void FixedUpdate()
    {
        body.velocity = velocity;
    }
}
