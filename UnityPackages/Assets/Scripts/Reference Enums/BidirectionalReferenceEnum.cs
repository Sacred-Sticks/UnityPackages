using UnityEngine;


[CreateAssetMenu(fileName = "Vector2 Enum", menuName = "Reference Enumerables/Vector2 Enum")]
public class BidirectionalReferenceEnum : ReferenceEnumerable
{
    public ReferenceEnumerable Up;
    public ReferenceEnumerable Down;
    public ReferenceEnumerable Left;
    public ReferenceEnumerable Right;
}