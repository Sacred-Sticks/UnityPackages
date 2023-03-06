using System;
using Essentials.Variables;
using UnityEngine;
namespace Essentials.References
{
    [Serializable]
    public class Vector3Reference
    {
        [SerializeField] private bool UseConstant = true;
        [SerializeField] private Vector3 ConstantValue;
        [SerializeField] private Vector3Variable Variable;

        public Vector3 Value
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
