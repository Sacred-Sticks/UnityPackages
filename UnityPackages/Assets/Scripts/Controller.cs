using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Controller : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
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
        switch (e.InputType)
        {
            case Enums.InputType.MoveForward:
                inputs.y = inputs.y < 0 ? 0 : 1;
                break;
            case Enums.InputType.MoveBackward:
                inputs.y = inputs.y > 0 ? 0 : -1;
                break;
            case Enums.InputType.MoveRight:
                inputs.x = inputs.x < 0 ? 0 : 1;
                break;
            case Enums.InputType.MoveLeft:
                inputs.x = inputs.x > 0 ? 0 : -1;
                break;
        }
        velocity = inputs.x * transform.right + inputs.y * transform.forward;
        velocity = velocity.normalized * speed;
    }

    private void RecievePlayerInputReleased(object sender, PlayerInputArguments e)
    {
        switch (e.InputType)
        {
            case Enums.InputType.MoveForward:
                inputs.y = inputs.y > 0 ? 0 : -1;
                break;
            case Enums.InputType.MoveBackward:
                inputs.y = inputs.y < 0 ? 0 : 1;
                break;
            case Enums.InputType.MoveRight:
                inputs.x = inputs.x > 0 ? 0 : -1;
                break;
            case Enums.InputType.MoveLeft:
                inputs.x = inputs.x < 0 ? 0 : 1;
                break;
        }
        velocity = inputs.x * transform.right + inputs.y * transform.forward;
        velocity = velocity.normalized * speed;
    }

    private void FixedUpdate()
    {
        body.velocity = velocity;
    }
}
