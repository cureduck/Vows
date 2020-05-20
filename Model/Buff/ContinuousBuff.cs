using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Model.Buff
{
    [Serializable]
    public abstract class ContinuousBuff : Buff
    {
        public abstract float baseDuration { get; }
        [ShowInInspector,HorizontalGroup("Group1",LabelWidth = 40),LabelText("Max"),ReadOnly]
        public float MaxDuration { get; protected set; }
        [ShowInInspector,HorizontalGroup("Group1",LabelWidth = 40),LabelText("Left")]
        public float durationLeft { get; protected set; }
        public float Progress =>1- durationLeft / MaxDuration;

        public Sprite icon;
        [ReadOnly] public Coroutine handler;

        protected virtual IEnumerator Wrapper(float durationMax, float indent = 1f)
        {
            OnStart();
            MaxDuration = durationMax;
            durationLeft = durationMax;

            while (durationLeft >= 0)
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
        }

        [Button]
        protected virtual void TakeOn(float timeMult=1)
        {
            base.TakeOn();
            handler = Owner.StartCoroutine(Wrapper(baseDuration * timeMult));
        }

        public override void TakeOn()
        {
            TakeOn(1);
        }

        protected override void TakeOff()
        {
            base.TakeOff();
            Owner.StopCoroutine(handler);
        }

        
    }
}