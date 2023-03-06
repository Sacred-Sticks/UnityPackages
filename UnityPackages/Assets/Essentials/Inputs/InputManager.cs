using System;
using System.Collections.Generic;
using Essentials.Events;
using Essentials.Variables;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Essentials.Inputs
{
    public class PlayerInputArguments : EventArgs
    {
        public GenericVariable Variable;
        public InputAction.CallbackContext Context { get; set; }
        public string Binding { get; set; }
    }

    [CreateAssetMenu]
    public class InputManager : ScriptableObject
    {
        [SerializeField] private EventChain inputEvent;
        [SerializeField] private List<InputActionsData> inputActionsData;
        [SerializeField] private List<InputAction> inputActions;
        
        [Serializable] private struct InputActionsData
        {
            public string binding;
            public InputActionType actionType;
            public GenericVariable variable;
        }

        private void OnEnable()
        {
            inputActions = new List<InputAction>();
            foreach (var inputData in inputActionsData)
            {
                var action = new InputAction(type: inputData.actionType, binding: inputData.binding);
                action.performed += context => CallInputEvents(context, inputData.variable);
                action.canceled += context => CallInputEvents(context, inputData.variable);
                SetActionPath(action, inputData.binding);
                inputActions.Add(action);
            }

            inputEvent.ChainedEvent += ReceiveInput;
        }

        private void CallInputEvents(InputAction.CallbackContext context, GenericVariable variable)
        {
            var args = new PlayerInputArguments
            {
                Context = context,
                Variable = variable,
            };
            inputEvent.CallEvent(this, args);
        }

        private static void SetActionPath(InputAction action, string binding)
        {
            action.ChangeBinding(0).WithPath(binding);
            action.Enable();
        }

        private static void ReceiveInput(object sender, EventArgs e)
        {
            if (e is not PlayerInputArguments inputArguments)
                return;
            if (inputArguments.Variable == null)
                return;
            if (inputArguments.Variable.GetType() == typeof(FloatVariable))
            {
                var variable = inputArguments.Variable as FloatVariable;
                variable!.Value = inputArguments.Context.ReadValue<float>();
            }

            if (inputArguments.Variable.GetType() == typeof(BoolVariable))
            {
                var variable = inputArguments.Variable as BoolVariable;
                variable!.Value = inputArguments.Context.ReadValue<bool>();
            }

            if (inputArguments.Variable.GetType() == typeof(IntVariable))
            {
                var variable = inputArguments.Variable as IntVariable;
                variable!.Value = inputArguments.Context.ReadValue<int>();
            }

            if (inputArguments.Variable.GetType() == typeof(Vector2Variable))
            {
                var variable = inputArguments.Variable as Vector2Variable;
                variable!.Value = inputArguments.Context.ReadValue<Vector2>();
            }

            if (inputArguments.Variable.GetType() == typeof(Vector3Variable))
            {
                var variable = inputArguments.Variable as Vector3Variable;
                variable!.Value = inputArguments.Context.ReadValue<Vector3>();
            }

        }
    }
}