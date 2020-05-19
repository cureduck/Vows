using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Model
{
    [Serializable]
    public class Item
    {
        public string name;
        public int amount = 1;
        [ShowInInspector]
        public virtual string className { get; }
        public virtual string itemDesc { get; }
        // 反射调用字段，不能更改命名
        public virtual Sprite itemSprite { get; internal set; }
        public virtual string spriteName { get; internal set; } = null;

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    
    public abstract class Consumpution : Item
    {
        public abstract void OnUse(Entity user);

        public virtual int MaxStack { get; } = 10;
        public int Qty;
    }
}

