using System;
using UnityEngine;

namespace View
{
    public class PanelTemplate<T1,T2>:MonoBehaviour
    {
        public T1 value;
        public T2[] subValue;

        public void SetValue(Icon<T1> icon)
        {
            value = icon.value;
        }
    }
}
