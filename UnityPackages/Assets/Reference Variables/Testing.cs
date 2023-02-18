using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private SingularReferenceEnum click;
    [Space(20)]
    [SerializeField] private FloatReference FloatVariable;
    [SerializeField] private IntReference IntVariable;
    [SerializeField] private BoolReference BoolVariable;
    [SerializeField] private Vector2Reference Vector2Variable;
    [SerializeField] private Vector3Reference Vector3Variable;

    private void Start()
    {
        inputManager.OnInputPressedEvent += InputManager_OnInputPressedEvent;
    }

    private void InputManager_OnInputPressedEvent(object sender, PlayerInputArguments e)
    {
        if (e.InputType == click.Variable)
        {
            FloatVariable.Value -= 1f;
            IntVariable.Value -= 1;
            BoolVariable.Value = !BoolVariable.Value;
            Vector2Variable.Value -= new Vector2(1, 1);
            Vector3Variable.Value -= new Vector3(1, 1, 1);
        }
    }
}
