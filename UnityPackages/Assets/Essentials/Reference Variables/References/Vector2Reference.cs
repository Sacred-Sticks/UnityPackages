using System;
using Essentials.Variables;
using UnityEngine;
namespace Essentials.References
{
    [Serializable]
    public class Vector2Reference
    {
        [SerializeField] private bool UseConstant = true;
        [SerializeField] private Vector2 ConstantValue;
        [SerializeField] private Vector2Variable Variable;

        public Vector2 Value
        {
            get
            {
                return UseConstant ? ConstantValue : Variable.Value;
            }
            set
            {
                if (UseConstant)
                {
                    ConstantValue = value;
                    return;
                }
                Variable.Value = value;
            }
        }
    }
}
