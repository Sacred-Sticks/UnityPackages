using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Kickstarter.Inputs
{
    public abstract class InputAssetObject : ScriptableObject
    {
        public abstract void Initialize();
        protected abstract void AddBindings();

        public event Action ValueChanged;
        
        protected void TriggerValueChanged()
        {
            ValueChanged?.Invoke();
        }
    }

    public abstract class InputAssetObject<TType> : InputAssetObject where TType : struct
    {
        public TType Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value;
                TriggerValueChanged();
            }
        }

        private TType value;
        protected InputAction inputAction;

        public override void Initialize()
        {
            inputAction = new InputAction(type: InputActionType.Value);
            AddBindings();
            inputAction.performed += ReceiveInput;
            inputAction.canceled += ReceiveInput;
            EnableInput();
        }

        public void EnableInput()
        {
            inputAction.Enable();
        }

        public void DisableInput()
        {
            inputAction.Disable();
        }

        private void ReceiveInput(InputAction.CallbackContext context)
        {
            Value = context.ReadValue<TType>();
        }
    }
}
