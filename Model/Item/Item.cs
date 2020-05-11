using System;
using UnityEngine;

namespace Model
{
    [Serializable]
    public abstract class Item
    {
        public virtual string ItemName { get; }
        public virtual string ItemDesc { get; }
        // 反射调用字段，不能更改命名
        public virtual Sprite ItemSprite { get; internal set; }
        public virtual string SpriteName { get; internal set; } = null;
    }

    
    public abstract class Consumpution : Item
    {
        public abstract void OnUse(Entity user);

        public virtual int MaxStack { get; } = 10;
        public int Qty;
    }
}

