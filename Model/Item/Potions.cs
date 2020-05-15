using System;
using UnityEngine;

namespace Model.Items
{
    public abstract class Potion : Item
    {
        public abstract int HpPoint { get; }
        public abstract int SpPoint { get; }

        public virtual void OnUse(Entity entity)
        {
            if (entity is Animal a)
            {
                a.HealHp(HpPoint);
                a.HealSp(SpPoint);
            }
        }
    }


    class hp : Potion
    {
        public override int HpPoint { get; } = 10;
        public override int SpPoint { get; } = 0;

        public override string className { get; }="Big Potion";

        public override string itemDesc { get; } = "Big Potion";

        public override Sprite itemSprite { get; internal set; }
    }

    class mp : Potion
    {
        public override int HpPoint { get; } = 3;
        public override int SpPoint { get; } = 0;

        public override string className { get; } = "Big Potion";

        public override string itemDesc { get; } = "Big Potion";

        public override Sprite itemSprite { get; internal set; }
    }

}
