using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Model.Buff
{
    public abstract class ReactBuff:ExclusiveBuff
    {
        [ShowInInspector]
        protected Entity Target;
        protected ReactBuff(Entity owner,Entity target, float timeMult = 1) : base(owner, timeMult)
        {
            this.Target = target;
        }
        
        protected override IEnumerator Wrapper(float durationMax, float indent = 1f)
        {
            OnStart(Owner);
            this.MaxDuration = durationMax;
            durationLeft = durationMax;

            while (durationLeft >= 0)
            {
                Effect();
                durationLeft -= indent;
                yield return new WaitForSeconds(indent);
            }
            OnComplete(Owner);
        }

        protected override void Bind()
        {
            Target.CastCompleted += OnComplete;
            Target.CastInterrupted += OnInterrupt;
            Owner.CastInterrupted += OnInterrupt;
        }

        protected override void Unbind()
        {
            Target.CastCompleted -= OnComplete;
            Target.CastInterrupted -= OnInterrupt;
            Owner.CastInterrupted -= OnInterrupt;
        }

        protected virtual void OnStart(Entity e)
        {
        }

        protected virtual void OnComplete(Entity e)
        {
            Debug.Log(Owner.name+"Complete");
            TakeOff();
            Owner.BroadcastComplete();
        }

        protected virtual void OnInterrupt(Entity e)
        {
            Debug.Log(Owner.name+"Interrupt");
            TakeOff();
        }
        

        public override void Interrupt()
        {
            OnInterrupt(Owner);
        }

        [Obsolete("use OnStart(Entity) instead",true)]
        protected override void OnStart()
        {
            base.OnStart();
        }


        [Obsolete("use OnComplete(Entity) instead",true)]
        protected override void OnComplete()
        {
            base.OnComplete();
        }

        [Obsolete("use OnInterrupt(Entity) instead",true)]
        protected override void OnInterrupt()
        {
            base.OnInterrupt();
        }
    }
}