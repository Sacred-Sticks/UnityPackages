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
            foreach (var player in players)
            {
                foreach (string binding in bindings)
                {
                    player.inputAction.AddBinding(binding);
                }
            }
        }
    }
}
