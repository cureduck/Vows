using System;
using UnityEngine;

namespace View
{
    public abstract class ListTemplate<T0,T1,T1Icon>:Icon<T0> where T1Icon:Icon<T1>
    {

        public virtual T1[] components { get; set; }
        public T1Icon template;
        [SerializeField] protected Transform childSection;


        protected override void UpdateUI()
        {
            foreach (Transform child in childSection)
            {
                Destroy(child.gameObject);
            }
            foreach (var item in components)
            {
                Instantiate(template, parent: childSection).value=item;
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
