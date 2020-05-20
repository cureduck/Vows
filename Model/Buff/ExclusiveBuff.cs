using System;
using System.Collections;
using UnityEngine;

namespace Model.Buff
{
    public abstract class ExclusiveBuff:ContinuousBuff
    {
        public ExclusiveBuff(Entity owner, float timeMult = 1) : base(owner, timeMult)
        {
        }

        protected override void Effect()
        {
            Owner.BroadCastProgress(Progress);
        }

        protected override void TakeOn(float timeMult)
        {
            Owner.status = Entity.Status.Reacting;
            base.TakeOn(timeMult);
            Bind();
        }

        protected virtual void Bind()
        {
            Owner.CastInterrupted += OnInterrupt;
        }

        protected virtual void Unbind()
        {
            Owner.CastInterrupted -= OnInterrupt;
        }

        protected override void TakeOff()
        {
            if (Owner.status == Entity.Status.Reacting) Owner.status = Entity.Status.Idle;
            Unbind();
            Owner.task = null;
            Owner.StopCoroutine(handler);
            Owner.BroadCastProgress(0);
        }

        protected override void OnComplete()
        {
            Owner.BroadcastComplete();
            base.OnComplete();
        }

        private void OnInterrupt(Entity e)
        {
            OnInterrupt();
        }
    }
}