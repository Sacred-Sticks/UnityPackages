using System.Collections.Generic;
using Essentials.Variables;
using UnityEngine;

namespace Essentials.Groups
{
    [CreateAssetMenu(fileName = "Variable List", menuName = "Variables/Groups/List")]
    public class VariableList : ScriptableObject
    {
        [SerializeField] private List<GenericVariable> variables;

        public List<GenericVariable> Variables
        {
            get
            {
                return variables;
            }
        }
    }
}
