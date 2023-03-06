using System;
using Essentials.Variables;
using UnityEngine;
namespace Essentials.References
{
    [Serializable]
    public class BoolReference
    {
        [SerializeField] private bool UseConstant = true;
        [SerializeField] private bool ConstantValue;
        [SerializeField] private BoolVariable Variable;

        public bool Value
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
