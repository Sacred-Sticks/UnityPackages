using UnityEngine;

[CreateAssetMenu(fileName = "BoolVariable", menuName = "Variables/BoolVariable")]
public class BoolVariable : ScriptableObject
{
    [SerializeField] private bool value;
    [SerializeField] private bool resetValue;
    [ConditionalHide("resetValue", true)]
    [SerializeField] private bool initialValue;

    public bool Value
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
