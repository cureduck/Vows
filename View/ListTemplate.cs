using System;
using UnityEngine;

namespace View
{
    class ListTemplate<T1,T2>:MonoBehaviour where T2:Icon<T1>
    {
        public T1[] source;
        public T2 template;


        public void Display()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
            foreach (var item in source)
            {
                Instantiate(template, transform);
            }
        }
    }
}
