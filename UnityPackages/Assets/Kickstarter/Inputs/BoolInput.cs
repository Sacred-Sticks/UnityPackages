using UnityEngine;
using UnityEngine.InputSystem;

namespace Kickstarter.Inputs
{
    [CreateAssetMenu(fileName = "Bool Input", menuName = "Inputs/Input Assets/Bool")]
    public class BoolInput : InputAssetObject<bool>
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
