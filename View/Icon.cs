using System;
using UnityEngine;

namespace View
{
    public abstract class Icon<T>:MonoBehaviour
    {
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
