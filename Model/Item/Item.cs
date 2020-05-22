﻿using System;
using Sirenix.OdinInspector;
using Unity.Collections;
using UnityEngine;

namespace Model
{
    [Serializable]
    public abstract class Item
    {
        public string name;
        [ShowInInspector]
        public virtual string className { get; }
        public virtual string itemDesc { get; }
        // 反射调用字段，不能更改命名
        [ShowInInspector,Sirenix.OdinInspector.ReadOnly]
        public virtual Sprite itemSprite { get; internal set; }
        public virtual string spriteName { get; internal set; } = null;
    }

    
    public abstract class Consumpution : Item
    {
        public abstract void OnUse(Entity user);

        public virtual int MaxStack { get; } = 10;
        public int Qty;
        public abstract void OnUse();
    }
}

