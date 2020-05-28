namespace Model.Buff
{
    public class EmptyBuff:ReactBuff
    {
        public EmptyBuff(Entity owner, Entity target, float timeMult = 1) : base(owner, target, timeMult)
        {
        }

        public override float baseDuration => 10f;
        protected override void Effect()
        {
        }
    }
}