namespace Model.Buff
{
    public abstract class ExclusiveBuff:ContinuousBuff
    {
        public ExclusiveBuff(Entity owner, float timeMult = 1) : base(owner, timeMult)
        {
        }

        protected override void OnStart()
        {
            if (Owner.status!=Entity.Status.Idle) return;
            Owner.status = Entity.Status.Reacting;
            Owner.ReactInturrpted += TakeOff;
            
            base.OnStart();
        }

        protected virtual void TakeOff(Entity self)
        {
            if (Owner.status == Entity.Status.Reacting) Owner.status = Entity.Status.Idle;
            Owner.ReactInturrpted -= TakeOff;

            Owner.task = null;
            Owner.StopCoroutine(handler);
        }

        protected override void OnComplete()
        {
            Owner.BroadcastComplete();
            base.OnComplete();
        }
    }
}