using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Model
{
    /// <summary>
    /// 交互时获得的持续式buff
    /// </summary>
    public abstract class ReactBuff: CoroBuff
    {
        public override void Bind(Entity user)
        {
            user.ReactCompleted += TakeOff;
            user.ReactInturrpted += TakeOff;
        }

        public override void TakeOff(Entity owner)
        {
            base.TakeOff(owner);
            owner.ReactCompleted -= TakeOff;
            owner.ReactInturrpted -= TakeOff;
        }

        public override void TakeOn(Entity user)
        {
            base.TakeOn(user);
        }

        public IEnumerator t()
        {
            yield return null;
        }
    }
  
}
