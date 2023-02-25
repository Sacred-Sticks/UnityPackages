using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerInputArguments : EventArgs
{
    public InputAction.CallbackContext Context { get; set; }
    public string Binding { get; set; }
    public GenericVariable Variable;
}

[CreateAssetMenu]
public class InputManager : ScriptableObject
{
    [Serializable]
    private struct InputActionsData
    {
        public string binding;
        public InputActionType actionType;
        public GenericVariable variable;
    }
    [SerializeField] private List<InputActionsData> inputActionsData;

    public delegate void OnInput(object sender, PlayerInputArguments e);

    public event OnInput OnInputEvent;
    public event OnInput OnInputPressedEvent;
    public event OnInput OnInputReleasedEvent;
    [SerializeField] private List<InputAction> inputActions;

    private void OnEnable()
    {
        inputActions = new List<InputAction>();
        InputAction action;
        foreach (var inputData in inputActionsData)
        {
            action = new(type: inputData.actionType, binding: inputData.binding);
            action.performed += context => CallInputEvents(context: context, inputData.variable);
            action.canceled += context => CallInputEvents(context: context, inputData.variable);
            SetActionPath(action: action, binding: inputData.binding);
            inputActions.Add(action);
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
