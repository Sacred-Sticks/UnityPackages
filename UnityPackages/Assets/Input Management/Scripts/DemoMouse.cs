using UnityEngine;

public class DemoMouse : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private SingularReferenceEnum lookInput;

    private void Start()
    {
        inputManager.OnInputEvent += OnPlayerInput;
    }

    private Vector2 mouseInput;

    private void OnPlayerInput(object sender, PlayerInputArguments e)
    {
        // Makeshift Switch Statement because SO's cannot be declared as const, therefore cannot be a case
        if (e.InputType == lookInput.Variable)
        {
            mouseInput = e.Context.ReadValue<Vector2>();
            goto InputReceived;
        }
        // Default Case Goes Here
        return;
    InputReceived:
        // Anything After Switch Statement still needed after modifying values goes here
        return;
    }
}
