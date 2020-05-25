using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Buff;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Model
{
    public abstract class Stru:Property
    {
        public virtual string StruName { get; }
        public enum State { Building, Done}
        [ShowInInspector]private float Progress;
        public float buildTime = 10f;
        private SpriteRenderer[] srs;
        public State state;
        protected abstract Action<Entity>[] DisiredReactions { get; }


        public void Awake()
        {
            srs= GetComponentsInChildren<SpriteRenderer>();
            if (state==State.Building)
            {
                SetAlpha(0.5f);
            }
        }

        private void SetAlpha(float alpha)
        {
            foreach (var sr in srs)
            {
                var color = sr.color;
                color.a = alpha;
                sr.color = color;
            }
        }

        protected void Build(Entity builder)
        {
            this.task=new Building(this, builder);
            builder.task=new Working(builder,this);
            
            this.task.TakeOn();
            builder.task.TakeOn();
        }

        private class Building:ReactBuff
        {
            public Building(Entity owner, Entity target, float timeMult = 1) : base(owner, target, timeMult)
            {
            }

            public override float baseDuration =>10f;

            protected override void Effect()
            {
                var s = (Owner as Stru);
                if (s != null) s.Progress += 1 / MaxDuration;
                base.Effect();
                if (s.Progress>1)
                {
                    OnComplete(Owner);
                }
            }

            protected override void OnComplete(Entity e)
            {
                var s = (Owner as Stru);
                s.CompleteBuild();
                base.OnComplete(e);
            }
        }
        
        private class Working:ReactBuff
        {
            public Working(Entity owner, Entity target, float timeMult = 1) : base(owner, target, timeMult)
            {
            }

            public override float baseDuration => 10f;
        }
        
        private void CompleteBuild()
        {
            SetAlpha(1f);
            state = State.Done;
        }


        public override Action<Entity>[] GetReactions(Entity sponser)
        {
            switch (state)
            {
                case State.Building:
                    return new Action<Entity>[1] { Build };
                case State.Done:
                    return DisiredReactions;
                default:
                    return null;
            }
        }
        
    }
}
