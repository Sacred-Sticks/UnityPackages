using UnityEngine;

[CreateAssetMenu(fileName = "Vector2Variable Enum", menuName = "Reference Enumerables/Vector2Variable Enum")]
public class BidirectionalReferenceEnum : ReferenceEnum
{
    public EnumerableObject Up;
    public EnumerableObject Down;
    public EnumerableObject Left;
    public EnumerableObject Right;
}