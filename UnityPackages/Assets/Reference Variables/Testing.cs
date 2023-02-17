using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private SingularReferenceEnum click;
    [Space(20)]
    [SerializeField] private FloatReference health;

    private void Start()
    {
        inputManager.OnInputPressedEvent += InputManager_OnInputPressedEvent;
    }

    private void InputManager_OnInputPressedEvent(object sender, PlayerInputArguments e)
    {
        if (e.InputType == click.Variable)
        {
            health.Value--;
        }
    }
}
