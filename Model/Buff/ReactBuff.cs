using Sirenix.OdinInspector;

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

        protected override void OnStart()
        {
            Target.ReactCompleted += OnComplete;
            Target.ReactInturrpted += OnInterrupt;
            base.OnStart();
        }


        protected virtual void OnComplete(Entity e)
        {
            TakeOff(e);
        }

        protected virtual void OnInterrupt(Entity e)
        {
            TakeOff(e);
        }

        protected override void TakeOff(Entity e)
        {
            Target.ReactCompleted -= OnComplete;
            Target.ReactInturrpted -= OnInterrupt;
            base.TakeOff(e);
        }
    }
}