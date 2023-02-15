using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Vector2 Enum", menuName = "Reference Enumerables/Vector2 Enum")]
public class BidirectionalReferenceEnum : ReferenceEnum
{
    public EnumerableObject Up;
    public EnumerableObject Down;
    public EnumerableObject Left;
    public EnumerableObject Right;
}