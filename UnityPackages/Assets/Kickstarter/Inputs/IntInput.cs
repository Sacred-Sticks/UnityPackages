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
