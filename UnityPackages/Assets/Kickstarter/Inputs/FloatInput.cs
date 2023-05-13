using UnityEngine;
using UnityEngine.InputSystem;

namespace Kickstarter.Inputs
{
    [CreateAssetMenu(fileName = "Float Input", menuName = "Inputs/Input Assets/Float")]
    public class FloatInput : InputAssetObject<float>
    {
        [SerializeField] private string[] bindings;
        protected override void AddBindings()
        {
            foreach (string binding in bindings)
            {
                inputAction.AddBinding(binding);
            }
        }
    }
}
