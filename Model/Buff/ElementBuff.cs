using Sirenix.OdinInspector.Editor;

namespace Model.Buff
{
    public abstract class ElementBuff:ContinuousBuff
    {
        public int Level { get; private set; }
        
        protected ElementBuff(Entity owner, int level, float timeMult = 1) : base(owner, timeMult)
        {
            Level = level;
        }

        public override void TakeOn()
        {
            var t = Owner.buffs.Find(match: buff => { return true; });

            if (t!=null)
            {
                
            }
            base.TakeOn();
        }
    }
}