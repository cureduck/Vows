using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.Collections;
using UnityEditor;
using UnityEngine;

namespace Model.Buff
{
    [Serializable]
    public abstract class Buff
    {
        [ShowInInspector]
        public Entity owner { get; set; }
        [ShowInInspector]
        public abstract float baseDuration { get; }
        [ShowInInspector]
        public float durationMax { get; set; }
        [ShowInInspector]
        public float durationLeft { get; private set; }
        public Sprite icon;
        [Sirenix.OdinInspector.ReadOnly]
        public Coroutine handler;

        protected IEnumerator Wrapper(float durationMax, float indent=1f)
        {
            var effect = Effect();
            this.durationMax = durationMax;
            durationLeft = durationMax;

            foreach (Action e in effect)
            {
                e.Invoke();
                durationLeft -= indent;
                yield return new WaitForSeconds(indent);
                if(durationLeft<0) yield break;
            }
            
        }

        protected abstract IEnumerable<Action> Effect();



        public Buff(Entity owner,float timeMult=1)
        {
            this.owner = owner;
            TakeOn(timeMult);
        }
        
        [Button]
        public virtual void TakeOn(float timeMult)
        {
            handler = owner.StartCoroutine(Wrapper(baseDuration*timeMult));
        }

        [Button]
        public virtual void TakeOff()
        {
            owner.StopCoroutine(handler);
            owner.buffs.Remove(this);
        }
    }

    public class ConstantHeal:Buff
    {
        public ConstantHeal(Entity owner, float timeMult = 1) : base(owner, timeMult)
        {
        }

        private const float BaseDuration = 10;
        public override float baseDuration => BaseDuration;

        protected override IEnumerable<Action> Effect()
        {
            while (true)
            {
                yield return () =>
                {
                    if (owner is Animal a)
                    {
                        a.HealHp(1);
                        Debug.Log(1);
                    }
                };
            }
        }
    }
    
}