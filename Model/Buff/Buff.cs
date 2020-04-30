using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// buff的本质是一个协程，或者是数据的暂时状态更改
    /// 因此要记得
    /// </summary>
    public abstract class Buff
    {
        public abstract string name { get; }
        public abstract string description { get; }

        public abstract void TakeOn(Entity owner);

        /// <summary>
        /// 将该buff从持有者的buff栏移除
        /// </summary>
        /// <param name="owner"></param>
        public virtual void TakeOff(Entity owner)
        {
            owner.buffs.Remove(this);
        }
    }






    public abstract class CoroBuff : Buff
    {
        protected IEnumerator coro;

        /// <summary>
        /// 启动buff协程
        /// </summary>
        /// <param name="user"></param>
        public abstract void StartCoro(Entity user);

        /// <summary>
        /// 将buff结束的动作TakeOff绑定到相应的事件上去,若为固定持续时间的，则
        /// </summary>
        /// <param name="user"></param>
        public abstract void Bind(Entity user);

        public override void TakeOn(Entity user)
        {
            StartCoro(user);
            Bind(user);
        }

        /// <summary>
        /// 持续施法类buff样本 
        /// </summary>
        /// <param name="owner"></param>
        public override void TakeOff(Entity owner)
        {
            base.TakeOff(owner);
            owner.StopCoroutine(coro);
        }
    }

    public abstract class Aura
    {
        public abstract string name { get; }
        public abstract string description { get; }

    }
}
