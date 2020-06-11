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
        public Sprite[] sprites;

        private SpriteRenderer _sr;
        
        [ShowInInspector] public float progress;
        [ShowInInspector,ReadOnly]
        public bool Ripe => progress >= 1;

        private int _parse => sprites.Length;
        
        public float GrowUpTime { get; } = 10;
        [ShowInInspector]
        public virtual Item[] Drop { get; }

        private void Start()
        {
            nameText.text = DisplayName;
            _sr = GetComponent<SpriteRenderer>();
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

            var t =(int)((_parse-1)*progress);
            _sr.sprite = sprites[t];

            Show();
        }

        private void Show()
        {
            progressText.text = progress.ToString("P");
        }
    }
}