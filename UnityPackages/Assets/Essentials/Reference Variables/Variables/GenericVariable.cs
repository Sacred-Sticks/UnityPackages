using System;
using Essentials.Attributes;
using UnityEngine;

namespace Essentials.Variables
{
    public class GenericVariable<TDataType> : GenericVariable
    {
        [SerializeField] private TDataType value;
        [SerializeField] private bool resetValue;
        [ConditionalHide("resetValue", true)]
        [SerializeField] private TDataType initialValue;
        
        public delegate void ValueDelegate(object sender, EventArgs e);

        public event ValueDelegate ValueChanged;
        
        public TDataType Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value;
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private void OnEnable()
        {
            if (resetValue)
                value = initialValue;
        }
    }

    public class GenericVariable : ScriptableObject
    {
    }
}