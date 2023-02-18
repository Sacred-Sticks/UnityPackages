using System;
using UnityEngine;

[Serializable]
public class Vector3Reference
{
    [SerializeField] private bool UseConstant = true;
    [SerializeField] private Vector3 ConstantValue;
    [SerializeField] private Vector3Variable Variable;

    public Vector3 Value
    {
        get => UseConstant ? ConstantValue : Variable.Value;
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
