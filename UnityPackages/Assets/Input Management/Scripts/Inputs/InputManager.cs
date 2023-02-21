using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputArguments : EventArgs
{
    public InputAction.CallbackContext Context { get; set; }
    public string Binding { get; set; }
    public EnumerableObject InputType { get; set; }
    public GenericVariable Variable;
}

[CreateAssetMenu]
public class InputManager : ScriptableObject
{
    [Serializable]
    private struct InputActionsData
    {
        public string Key;
        public string Binding;
        public InputActionType ActionType;
        public GenericVariable Variable;
    }
    [SerializeField] private List<InputActionsData> inputActionsData;

    public delegate void OnInput(object sender, PlayerInputArguments e);

    public event OnInput OnInputEvent;
    public event OnInput OnInputPressedEvent;
    public event OnInput OnInputReleasedEvent;
    public List<InputAction> InputActions { get; private set; }

    private void OnEnable()
    {
        InputActions = new();
        InputAction action;
        foreach (var inputData in inputActionsData)
        {
            action = new(type: inputData.ActionType, binding: inputData.Binding);
            action.performed += context => CallInputEvents(context: context, inputData.Variable);
            action.canceled += context => CallInputEvents(context: context, inputData.Variable);
            SetActionPath(action: action, binding: inputData.Binding);
            InputActions.Add(action);
        }

        OnInputEvent += ReceiveInput;
    }

    private void CallInputEvents(InputAction.CallbackContext context, GenericVariable variable)
    {
        PlayerInputArguments args = new()
        {
            Context = context,
            Variable = variable,
        };
        OnInputEvent?.Invoke(this, args);
        if (context.performed)
        {
            OnInputPressedEvent?.Invoke(this, args);
            return;
        }
        if (context.canceled)
        {
            OnInputReleasedEvent?.Invoke(this, args);
        }
    }

    private void SetActionPath(InputAction action, string binding)
    {
        action.ChangeBinding(0).WithPath(binding);
        action.Enable();
    }

    private void ReceiveInput(object sender, PlayerInputArguments e)
    {
        if (e.Variable == null)
            return;
        if (e.Variable.GetType() == typeof(FloatVariable))
        {
            var variable = e.Variable as FloatVariable;
            variable.Value = e.Context.ReadValue<float>();
        }

        if (e.Variable.GetType() == typeof(BoolVariable))
        {
            var variable = e.Variable as BoolVariable;
            variable.Value = e.Context.ReadValue<bool>();
        }

        if (e.Variable.GetType() == typeof(IntVariable))
        {
            var variable = e.Variable as IntVariable;
            variable.Value = e.Context.ReadValue<int>();
        }

        if (e.Variable.GetType() == typeof(Vector2Variable))
        {
            var variable = e.Variable as Vector2Variable;
            variable.Value = e.Context.ReadValue<Vector2>();
        }

        if (e.Variable.GetType() == typeof(Vector3Variable))
        {
            var variable = e.Variable as Vector3Variable;
            variable.Value = e.Context.ReadValue<Vector3>();
        }

    }
}
