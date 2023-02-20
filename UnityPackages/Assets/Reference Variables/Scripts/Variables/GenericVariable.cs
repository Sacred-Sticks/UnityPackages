using UnityEngine;

public class GenericVariable<TDataType> : GenericVariable
{
    [SerializeField] private TDataType value;
    [SerializeField] private bool resetValue;
    [ConditionalHide("resetValue", true)]
    [SerializeField] private TDataType initialValue;

    public TDataType Value
    {
        get => value;
        set => this.value = value;
    }

    private void OnEnable()
    {
        if (resetValue)
            value = initialValue;
    }

}

public class GenericVariable : ScriptableObject
{

}
