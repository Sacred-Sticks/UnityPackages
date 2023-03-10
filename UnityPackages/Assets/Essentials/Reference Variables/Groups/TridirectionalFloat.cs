using UnityEngine;
using Essentials.Variables;

namespace Essentials.Groups
{
    [CreateAssetMenu(fileName = "3D Float", menuName = "Variables/Groups/Tridirectional Float")]
    public class TridirectionalFloat : BidirectionalFloat
    {
        public FloatVariable Forward;
        public FloatVariable Backward;

        public float Z
        {
            get
            {
                return Forward.Value - Backward.Value;
            }
        }
    }
}
