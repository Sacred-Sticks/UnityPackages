using UnityEngine;

[CreateAssetMenu(fileName = "Float", menuName = "Variables/Float")]
public class FloatVariable : ScriptableObject
{
    [SerializeField] private float value;

    public float Value
    {
        get => value;
        set => this.value = value;
    }
}
