using UnityEngine;

[CreateAssetMenu(fileName = "Vector3Variable", menuName = "Variables/Vector3Variable")]
public class Vector3Variable : ScriptableObject
{
    [SerializeField] private Vector3 value;
    [SerializeField] private bool resetValue;
    [ConditionalHide("resetValue", true)]
    [SerializeField] private Vector3 initialValue;

    public Vector3 Value
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
