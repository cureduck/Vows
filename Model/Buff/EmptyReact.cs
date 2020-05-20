namespace Model.Buff
{
    public class EmptyReact:ReactBuff
    {
        public EmptyReact(Entity owner, Entity target, float timeMult = 1) : base(owner, target, timeMult)
        {
        }

        public override float baseDuration => 10;
    }
}