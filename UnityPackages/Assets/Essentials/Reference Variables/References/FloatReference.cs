using System;
using Essentials.Variables;
using UnityEngine;
namespace Essentials.References
{
    [Serializable]
    public class FloatReference
    {
        [SerializeField] private bool UseConstant = true;
        [SerializeField] private float ConstantValue;
        [SerializeField] private FloatVariable Variable;

        public float Value
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
