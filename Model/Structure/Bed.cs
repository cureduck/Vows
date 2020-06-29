using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Model.Buff;

namespace Model
{
    public class Bed : Stru
    {
        public override string StruName => "Bed";

        protected override Action<Animal>[] DesiredReactions => new Action<Animal>[1] { Sleep };

        private class SleepBuff:ReactBuff
        {
            public SleepBuff(Entity owner, Entity target, float timeMult = 1) : base(owner, target, timeMult)
            {
            }

            protected override void Effect()
            {
                if (Owner is Animal a)
                {
                    a.HealHp(3);
                    a.HealSp(1);
                }
                base.Effect();
            }

            public override float baseDuration { get; } = 10f;
        }


        
        

        public void Sleep(Entity user)
        {
            var t= new SleepBuff(user, this, 1);
            user.task = t;
            t.TakeOn();
            var t2=new EmptyBuff(this,user,1);
            this.task = t2;
            t2.TakeOn();
        }
    }
}
