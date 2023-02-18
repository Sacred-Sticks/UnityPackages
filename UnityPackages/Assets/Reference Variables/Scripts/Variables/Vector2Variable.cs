using UnityEngine;

[CreateAssetMenu(fileName = "Vector2Variable", menuName = "Variables/Vector2Variable")]
public class Vector2Variable : ScriptableObject
{
    [SerializeField] private Vector2 value;
    [SerializeField] private bool resetValue;
    [ConditionalHide("resetValue", true)]
    [SerializeField] private Vector2 initialValue;

    public Vector2 Value
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
