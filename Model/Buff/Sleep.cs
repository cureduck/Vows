namespace Model.Buff
{
    public class Sleep:ReactBuff
    {
        public Sleep(Entity owner, Entity target, float timeMult = 1) : base(owner, target, timeMult)
        {
        }

        public override float baseDuration => 10f;
        protected override void Effect()
        {
            if (Owner is Animal a)
            {
                a.HealHp(1);
            }
        }
    }
}