namespace Model.Buff
{
    public class Cast:ExclusiveBuff
    {
        public Cast(Entity owner, float timeMult = 1) : base(owner, timeMult)
        {
        }

        public override float baseDuration => 10f;
        protected override void Effect()
        {
            if (Owner is Animal a)
            {
                a.HealHp(1);
            }
            base.Effect();
        }
    }
}