using UnityEngine;
using UnityEngine.InputSystem;

namespace Kickstarter.Inputs
{
    [CreateAssetMenu(fileName = "Int Input", menuName = "Inputs/Input Assets/Int")]
    public class IntInput : InputAssetObject<int>
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
