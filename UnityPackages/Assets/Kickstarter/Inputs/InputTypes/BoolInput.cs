using UnityEngine;
using UnityEngine.InputSystem;

namespace Kickstarter.Inputs
{
    [CreateAssetMenu(fileName = "Bool Input", menuName = "Inputs/Input Assets/Bool")]
    public sealed class BoolInput : InputAssetObject<bool>
    {
        protected override void AddBindings()
        {
            foreach (string binding in bindings)
            {
                inputAction.AddBinding(binding);
            }
        }
    }
}
