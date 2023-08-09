using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EtienneSibeaux.Misc
{
    [DefaultExecutionOrder(-30)]
    public abstract class CA_ReferenceGetter : MonoBehaviour
    {
        private void Awake()
        {
            GetReferences();
        }

        protected virtual void GetReferences()
        { }
    }
}