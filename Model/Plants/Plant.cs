using System;
using System.Globalization;
using System.Reflection;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Model.Plants
{
    public class Plant:Property
    {
        
        [SerializeField] private Text nameText;
        [SerializeField] private Text progressText;
        
        public virtual string DisplayName { get;}

        [ShowInInspector] public float progress;
        [ShowInInspector,ReadOnly]
        public bool Ripe => progress >= 1;
        public float GrowUpTime { get; } = 10;
        [ShowInInspector]
        public virtual Item[] Drop { get; }

        private void Start()
        {
            nameText.text = DisplayName;
            InvokeRepeating("GrowUp",1,1);
        }
        
        public override Action<Animal>[] GetReactions(Animal sponser)
        {
            return !Ripe ? null : new Action<Animal>[1]{PickUp};
        }

        protected virtual void PickUp(Animal picker)
        {
            foreach (var item in Drop)
            {
                var clone =(Item) item.Clone();
                picker.PickUp(clone);
            }

            progress = 0;
        }

        private void GrowUp()
        {
            progress += 1 / GrowUpTime;
            if (progress > 1)
            {
                progress = 1;
            }

            Show();
        }

        private void Show()
        {
            progressText.text = progress.ToString("P");
        }
    }
}