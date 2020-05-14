using System;
using UnityEngine;

namespace View
{
    public abstract class ListTemplate<T0,T1,T1Icon>:Icon<T0> where T1Icon:Icon<T1>
    {

        public virtual T1[] components { get; set; }
        public T1Icon template;
        [SerializeField] protected Transform ChildSection;


        protected override void UpdateUI()
        {
            foreach (Transform child in ChildSection)
            {
                Destroy(child.gameObject);
            }
            foreach (var item in components)
            {
                Instantiate(template, parent: ChildSection).value=item;
            }
        }

        public virtual void AddNewTemplate()
        {
            Instantiate(template, parent: ChildSection);
        }
    }
}
