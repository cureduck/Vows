using System;
using UnityEngine;

namespace Model.Items
{
    public abstract class Potion : Consumpution
    {
        public abstract int HpPoint { get; }
        public abstract int SpPoint { get; }

        public override void OnUse(Entity entity)
        {
            if (!(entity is Animal a)) return;
            a.HealHp(HpPoint);
            a.HealSp(SpPoint);
            base.OnUse(entity);
        }
    }


    class hp : Potion
    {
        public override int HpPoint => 10;

        public override int SpPoint => 0;

        public override string className { get; }="Big Potion";

        public override string itemDesc { get; } = "Big Potion";
        
        private static Sprite _sprite;
        public override Sprite itemSprite
        {
            get=>_sprite;
            internal set { _sprite = value; }
        }
        
    }

    class mp : Potion
    {
        public override int HpPoint =>0;
        public override int SpPoint =>3;

        public override string className { get; } = "Big Potion";

        public override string itemDesc { get; } = "Big Potion";

        private static Sprite _sprite;
        public override Sprite itemSprite
        {
            get=>_sprite;
            internal set { _sprite = value; }
        }
    }

}
