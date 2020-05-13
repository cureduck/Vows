using System;
using UnityEngine;

namespace Model
{
    public class PanelTemplate<T1,T2>:MonoBehaviour
    {
        public T1 value;
        public T2[] subValue;
    }
}
