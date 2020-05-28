using System;
using JetBrains.Annotations;
using UnityEngine;

namespace View
{
    public abstract class ListTemplate<T0, T1, T1Icon> : Icon<T0> where T1Icon:Icon<T1>

    {
        protected virtual T1[] components { get; set; }
        public T1Icon template;
        [SerializeField] protected Transform childSection;


        protected override void UpdateUI()
        {
            foreach (Transform child in childSection)
            {
                Destroy(child.gameObject);
            }

            if (components==null) return;

            for (var i = 0; i < components.Length; i++)
            {
                var t = Instantiate(template, parent: childSection);
                t.value = components[i];
                t.index = i;
            }
        }

        protected virtual T1Icon AddNewTemplate()
        {
            return Instantiate(template, parent: childSection);
        }

        public virtual void AddNewItem()
        {
            AddNewTemplate();
        }
    }
}