using System;
using System.Collections.Generic;
using System.Linq;
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
        public abstract void AddDevice(InputDevice device);
        public abstract void RemoveDevice(InputDevice device);

        public enum PlayerRegister
        {
            KeyboardMouse,
            ControllerOne,
            ControllerTwo,
            ControllerThree,
            ControllerFour,
        }
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
            CreateActionMappings(devices);
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
            if (actionMap.ContainsKey(device))
                actionMap[device]?.Invoke(value);
        }

        private void StoreDevices(IReadOnlyList<InputDevice> inputDevices)
        {
            devices[0] = Keyboard.current;
            for (int i = 1; i < devices.Length; i++)
            {
                devices[i] = inputDevices[i - 1];
            }
        }

        private void CreateActionMappings(InputDevice[] inputDevices)
        {
            foreach (var inputDevice in inputDevices)
            {
                actionMap.Add(inputDevice, null);
            }
        }

        private void AdjustActionMappings(InputDevice[] inputDevices, InputDevice device)
        {
            InputDevice deviceToOverride = null;
            foreach (var key in actionMap.Keys.Where(key => Array.IndexOf(inputDevices, key) == -1))
            {
                deviceToOverride = key;
                break;
            }
            if (deviceToOverride == null)
                return;
            var action = actionMap[deviceToOverride];
            actionMap.Remove(deviceToOverride);
            actionMap.Add(device, action);
        }

        public void SubscribeToInputAction(Action<TType> action, PlayerRegister playerRegister)
        {
            int playerIndex = playerRegister switch
            {
                PlayerRegister.KeyboardMouse => 0,
                PlayerRegister.ControllerOne => 1,
                PlayerRegister.ControllerTwo => 2,
                PlayerRegister.ControllerThree => 3,
                PlayerRegister.ControllerFour => 4,
                _ => throw new ArgumentOutOfRangeException(nameof(playerRegister), playerRegister, null)
            };
            actionMap[devices[playerIndex]] += action;
        }

        public override void AddDevice(InputDevice device)
        {
            for (int i = 0; i < devices.Length; i++)
            {
                if (devices[i] != null)
                    continue;
                devices[i] = device;
                AdjustActionMappings(devices, device);
                break;
            }
        }

        public override void RemoveDevice(InputDevice device)
        {
            var eligibleDeviceReplacements = new List<InputDevice>();
            if (Keyboard.current != null)
                eligibleDeviceReplacements.Add(Keyboard.current);
            eligibleDeviceReplacements.AddRange(Gamepad.all);
            for (int i = 0; i < devices.Length; i++)
            {
                if (eligibleDeviceReplacements.Contains(devices[i]))
                    eligibleDeviceReplacements.Remove(devices[i]);
                if (devices[i] != device)
                    continue;
                var newDevice = eligibleDeviceReplacements.FirstOrDefault();
                devices[i] = newDevice;
                AdjustActionMappings(devices, newDevice);
                break;
            }
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
