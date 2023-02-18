using UnityEngine;

[CreateAssetMenu(fileName = "IntVariable", menuName = "Variables/IntVariable")]
public class IntVariable : ScriptableObject
{
    [SerializeField] private int value;
    [SerializeField] private bool resetValue;
    [ConditionalHide("resetValue", true)]
    [SerializeField] private int initialValue;

    public int Value
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
