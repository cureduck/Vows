using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Model
{
    public abstract class Structure:Entity
    {
        public virtual string StruName { get; }
        public enum State { Building, Done}
        [SerializeField]private float Progress;
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

        protected IEnumerator BuildPro(Entity builder)
        {
            while (Progress <1f)
            {
                Progress += Time.deltaTime / buildTime;
                yield return null;
            }
            CompleteBuild();
            builder.OnReactCompleted(builder);
            this. OnReactCompleted(this);
        }

        protected void Build(Entity builder)
        {
            InvokeReact(BuildPro(builder),builder);
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
