using System;
using UnityEngine;

namespace Model
{
    [Serializable]
    public abstract class Item
    {
        public abstract string ItemName { get; }
        public abstract string ItemDesc { get; }
        /// <summary>
        /// 反射调用字段，不能更改命名
        /// </summary>
        public abstract Sprite ItemSprite { get; internal set; }
        public virtual string SpriteName { get; internal set; } = null;
    }

    
    public abstract class Consumpution : Item
    {
        public abstract void OnUse(Entity user);

        public static int a { get; set; }
    }
}

