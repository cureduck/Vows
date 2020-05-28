using System;
using UnityEngine;

namespace View
{
    public abstract class Icon<T>:MonoBehaviour
    {
        public int index = 0;
        public T _value;
        public virtual T value 
        { 
            get => _value;
            set
            {
                _value = value;
                UpdateUI();
            }
        }

        protected abstract void UpdateUI();


    }
}
