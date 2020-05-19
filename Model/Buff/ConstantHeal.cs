using UnityEngine;

namespace Model.Buff
{
    public class ConstantHeal:ContinuousBuff
    {
        public ConstantHeal(Entity owner, float timeMult = 1) : base(owner, timeMult)
        {
        }

        private static float defaultDuration = 10;

        public override float baseDuration => defaultDuration;

        protected override void Effect()
        {
            if (Owner is Animal a)
            {
                Debug.Log(1);
                a.HealHp(1);
            }
        }
    }
}