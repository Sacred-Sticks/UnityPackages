using UnityEngine;
using Essentials.Variables;

namespace Essentials.Groups
{
    [CreateAssetMenu(fileName = "3D Float", menuName = "Variables/Groups/Tridirectional Float")]
    public class TridirectionalFloat : BidirectionalFloat
    {
        [SerializeField] protected FloatVariable forward;
        [SerializeField] protected FloatVariable backward;
        
        public float Forward
        {
            get
            {
                return forward.Value;
            }
            set
            {
                forward.Value = value;
            }
        }
        public float Backward
        {
            get
            {
                return backward.Value;
            }
            set
            {
                backward.Value = value;
            }
        }

        public float Z
        {
            get
            {
                return Forward - Backward;
            }
        }
    }
}
