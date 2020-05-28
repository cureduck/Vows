using System;
using System.Globalization;
using System.Reflection;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Model.Plants
{
    public abstract class Plant:Property
    {
        
        [SerializeField] private Text nameText;
        [SerializeField] private Text progressText;
        
        public abstract string DisplayName { get;}
        
        [ShowInInspector]
        public float Progress { get; private set; }
        public bool Ripe => Progress >= 1;
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

            Progress = 0;
        }

        private void GrowUp()
        {
            Progress += 1 / GrowUpTime;
            if (Progress > 1)
            {
                Progress = 1;
            }

            Show();
        }

        private void Show()
        {
            progressText.text = Progress.ToString("P");
        }
    }
}