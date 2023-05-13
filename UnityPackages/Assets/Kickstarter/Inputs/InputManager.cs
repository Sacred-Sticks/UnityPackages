using UnityEngine;

namespace Kickstarter.Inputs
{
    [CreateAssetMenu(fileName = "Input Manager", menuName = "Inputs/Input Manager")]
    public class InputManager : ScriptableObject
    {
        [SerializeField] private InputAssetObject[] inputObjects;

        public void Initialize()
        {
            foreach (var inputObject in inputObjects)
            {
                inputObject.Initialize();
            }
        }
    }
}