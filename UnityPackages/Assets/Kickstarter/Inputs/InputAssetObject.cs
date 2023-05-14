using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Kickstarter.Inputs
{
    public abstract class InputAssetObject : ScriptableObject
    {
        public abstract void Initialize(Gamepad[] controllers);
        protected abstract void AddBindings();
        public abstract void EnableInput();
        public abstract void DisableInput();
    }

    public abstract class InputAssetObject<TType> : InputAssetObject where TType : struct
    {
        protected class PlayerInput
        {
            public PlayerInput(InputDevice[] inputDevice, InputAction inputAction)
            {
                inputDevices = inputDevice;
                this.inputAction = inputAction;
                inputAction.performed += ReceiveInput;
                inputAction.canceled += ReceiveInput;
            }

            public Action<TType> ValueChanged { get; set; }

            public InputAction inputAction { get; }
            public InputDevice[] inputDevices { get; }

            public void ReceiveInput(InputAction.CallbackContext context)
            {
                if (inputDevices.Any(inputDevice => inputDevice == context.control.device))
                {
                    ValueChanged?.Invoke(context.ReadValue<TType>());
                }
            }
        }

        protected PlayerInput[] players;

        private Gamepad[] gamepads;

        public override void Initialize(Gamepad[] controllers)
        {
            gamepads = controllers;
            players = new PlayerInput[controllers.Length + 1];
            var keyboardMouse = new InputDevice[]
            {
                InputSystem.GetDevice<Keyboard>(),
                InputSystem.GetDevice<Mouse>(),
            };
            players[0] = new PlayerInput(keyboardMouse, new InputAction(name: name, type: InputActionType.Value));
            for (int i = 1; i < players.Length; i++)
            {
                var gamepad = new InputDevice[]
                {
                    gamepads[i - 1],
                };
                players[i] = new PlayerInput(gamepad, new InputAction(name: name, type: InputActionType.Value));
            }
            AddBindings();
        }

        public void SubscribeToInputAction(Action<TType> action, int playerIndex = 0)
        {
            if (playerIndex >= 0 && playerIndex < players.Length)
                players[playerIndex].ValueChanged += action;
        }

        public override void EnableInput()
        {
            foreach (var playerInput in players)
            {
                playerInput.inputAction.Enable();
            }
        }

        public override void DisableInput()
        {
            foreach (var playerInput in players)
            {
                playerInput.inputAction.Disable();
            }
        }
    }
}
