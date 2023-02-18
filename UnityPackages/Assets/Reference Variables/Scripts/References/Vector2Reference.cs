using System;
using UnityEngine;

[Serializable]
public class Vector2Reference
{
    [SerializeField] private bool UseConstant = true;
    [SerializeField] private Vector2 ConstantValue;
    [SerializeField] private Vector2Variable Variable;

    public Vector2 Value
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
