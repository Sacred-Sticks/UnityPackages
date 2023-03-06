using UnityEngine;
using Essentials.Variables;
using UnityEngine.Serialization;

namespace Essentials.Groups
{
    [CreateAssetMenu(fileName = "2D Float", menuName = "Variables/Groups/Bidirectional Float")]
    public class BidirectionalFloat : DirectionalFloat
    {
        [SerializeField] protected FloatVariable right;
        [SerializeField] protected FloatVariable left;

        public float Right
        {
            get
            {
                return right.Value;
            }
            set
            {
                right.Value = value;
            }
        }
        public float Left
        {
            get
            {
                return left.Value;
            }
            set
            {
                left.Value = value;
            }
        }

        public float X
        {
            get
            {
                return Right - Left;
            }
        }
    }
}
