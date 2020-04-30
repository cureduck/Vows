using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Model
{
    public abstract class ItemBase
    {
        public virtual string ItemName { get; }
        public string description;

        private static Sprite _sprite;
        public Sprite sprite { get => _sprite;}

        public static void Set(Sprite spriteName)
        {
            _sprite = spriteName;
        }
    }


    public abstract class Equipment : ItemBase
    {

    }

    public abstract class Weapon : Equipment
    {

    }

    public class Armor : Equipment
    {
        public float def;

    }

    public abstract class Consumpution : ItemBase
    {
        public abstract int capacity { get; }
        public int count { get; private set; }


        public virtual void Use(Animal user)
        {
            count -= 1;
            
        }

        public Consumpution(int count=10)
        {
            this.count = count;
        }
    }
}
