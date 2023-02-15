using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Vector2 Enum", menuName = "Reference Enumerables/Vector2 Enum")]
public class BidirectionalReferenceEnum : ReferenceEnum
{
    [SerializeField] private Dictionary<Enums.Direction, EnumerableObject> enumeratedDictionary; 

    public Dictionary<Enums.Direction, EnumerableObject> Enum
    {
        get => enumeratedDictionary;
    }
}
