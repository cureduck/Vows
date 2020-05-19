using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Model.Buff
{
    public class ReactBuff:Buff
    {
        [ShowInInspector]
        private Entity target;
        
        public ReactBuff(Entity owner,Entity target, float timeMult = 1) : base(owner, timeMult)
        {
            this.target = target;
        }

        public void TakeOff(Entity target)
        {
            target.ReactCompleted -= TakeOff;
            target.ReactInturrpted -= TakeOff;
            base.TakeOff();
        }

        public override void TakeOn(float timeMult)
        {
            target.ReactCompleted += TakeOff;
            target.ReactInturrpted += TakeOff;
            
            base.TakeOn(timeMult);
        }

        public override float baseDuration { get; }
        protected override IEnumerable<Action> Effect()
        {
            throw new NotImplementedException();
        }
    }
}