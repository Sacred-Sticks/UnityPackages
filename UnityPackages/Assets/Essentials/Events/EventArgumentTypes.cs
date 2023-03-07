using System;
using System.Collections.Generic;
using UnityEngine;

namespace Essentials.Events
{
    public class DataArgs : EventArgs
    {
        public List<MonoBehaviour> MonoBehaviours
        {
            get;
            set;
        }

        public DataArgs(List<MonoBehaviour> dataset)
        {
            MonoBehaviours = dataset;
        }
    }
}