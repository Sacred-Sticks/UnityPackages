using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Kickstarter.Inputs
{
    public abstract class InputAssetObject : ScriptableObject
    {
        public abstract void Initialize(Gamepad[] gamepads);
        protected abstract void AddBindings();
        public abstract void EnableInput();
        public abstract void DisableInput();
    }

    public abstract class InputAssetObject<TType> : InputAssetObject where TType : struct
    {
        protected readonly Dictionary<InputDevice, Action<TType>> actionMap = new Dictionary<InputDevice, Action<TType>>();
        private InputDevice[] devices;
        protected InputAction inputAction;

        public override void Initialize(Gamepad[] gamepads)
        {
            devices = new InputDevice[gamepads.Length + 1];
            inputAction = new InputAction(name: name, type: InputActionType.PassThrough);
            AddBindings();
            AddRegistration();
            StoreDevices(gamepads);
            CreateActionMappings(gamepads);
        }

        private void AddRegistration()
        {
            inputAction.performed += RegisterInput;
            inputAction.canceled += RegisterInput;
        }

        private void RegisterInput(InputAction.CallbackContext context)
        {
            var value = context.ReadValue<TType>();
            var device = context.control.device;
            
            switch (device)
            {
                case Mouse:
                case Keyboard:
                    actionMap[Keyboard.current]?.Invoke(value);
                    break;
                default:
                    if (actionMap.ContainsKey(device))
                        actionMap[device]?.Invoke(value);
                    break;
            }
        }

        private void StoreDevices(IReadOnlyList<Gamepad> gamepads)
        {
            devices[0] = Keyboard.current;
            for (int i = 1; i < devices.Length; i++)
            {
                devices[i] = gamepads[i - 1];
            }
        }

        private void CreateActionMappings(Gamepad[] gamepads)
        {
            actionMap.Add(Keyboard.current, null);
            foreach (var gamepad in gamepads)
            {
                actionMap.Add(gamepad, null);
            }
        }

        public void SubscribeToInputAction(Action<TType> action, int playerIndex = 0)
        {
            if (playerIndex > devices.Length - 1)
                return;
            actionMap[devices[playerIndex]] += action;
        }

        public override void EnableInput()
        {
            inputAction.Enable();
        }

        public override void DisableInput()
        {
            inputAction.Enable();
        }
    }
}
