using System;
using UnityEngine;

[Serializable]
public class FloatReference
{
    [SerializeField] private bool UseConstant = true;
    [SerializeField] private float ConstantValue;
    [SerializeField] private FloatVariable Variable;

    public float Value
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
