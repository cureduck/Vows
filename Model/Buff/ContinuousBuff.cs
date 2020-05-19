using System;
using System.Collections;
using System.ComponentModel;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Model.Buff
{
    [Serializable]
    public abstract class ContinuousBuff : Buff
    {
        public abstract float baseDuration { get; }
        [ShowInInspector,HorizontalGroup("Group1",LabelWidth = 40),LabelText("Max"),Sirenix.OdinInspector.ReadOnly]
        public float MaxDuration { get; private set; }
        [ShowInInspector,HorizontalGroup("Group1",LabelWidth = 40),LabelText("Left")]
        public float durationLeft { get; private set; }
        public float Progress => durationLeft / MaxDuration;

        public Sprite icon;
        [Sirenix.OdinInspector.ReadOnly] public Coroutine handler;

        protected IEnumerator Wrapper(float durationMax, float indent = 1f)
        {
            OnStart();
            this.MaxDuration = durationMax;
            durationLeft = durationMax;

            while (durationLeft > 0)
            {
                Effect();
                durationLeft -= indent;
                yield return new WaitForSeconds(indent);
            }
            OnComplete();
        }


        protected abstract void Effect();

        public ContinuousBuff(Entity owner, float timeMult = 1) : base(owner)
        {
            TakeOn(timeMult);
        }

        [Button]
        protected void TakeOn(float timeMult)
        {
            base.TakeOn();
            handler = Owner.StartCoroutine(Wrapper(baseDuration * timeMult));
        }

        protected override void TakeOff()
        {
            base.TakeOff();
            Owner.StopCoroutine(handler);
        }

        
    }
}