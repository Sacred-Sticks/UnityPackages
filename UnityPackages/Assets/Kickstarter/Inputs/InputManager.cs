using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Kickstarter.Inputs
{
    [CreateAssetMenu(fileName = "Input Manager", menuName = "Inputs/Input Manager")]
    public class InputManager : ScriptableObject
    {
        [Range(0, 4)]
        [SerializeField] private int maxPlayerCount;
        [Space(20)]
        [SerializeField] private InputAssetObject[] inputObjects;
        
        public void Initialize()
        {
            var gamepads = Gamepad.all.Take(maxPlayerCount).ToArray();
            foreach (var inputObject in inputObjects)
            {
                inputObject.Initialize(gamepads);
            }
            EnableAll();
        }

        private void EnableAll()
        {
            foreach (var inputObject in inputObjects)
            {
                inputObject.EnableInput();
            }
        }

        public void DisableAll()
        {
            foreach (var inputObject in inputObjects)
            {
                inputObject.DisableInput();
            }
        }

        public void OnDisable()
        {
            //DisableAll();
        }
    }
}