using UnityEngine;
using Essentials.Variables;
using UnityEngine.Serialization;

namespace Essentials.Groups
{
    [CreateAssetMenu(fileName = "1D Float", menuName = "Variables/Groups/Directional Float")]
    public class DirectionalFloat : ScriptableObject
    {
        [SerializeField] protected FloatVariable up;
        [SerializeField] protected FloatVariable down;
        
        public float Up
        {
            get
            {
                return up.Value;
            }
            set
            {
                up.Value = value;
            }
        }
        public float Down
        {
            get
            {
                return down.Value;
            }
            set
            {
                down.Value = value;
            }
        }

        public float Y
        {
            get
            {
                return Up - Down;
            }
        }
    }
}
