using System;
using UnityEngine;

namespace Model
{
    [Serializable]
    public class Item:ScriptableObject
    {
        public int ID;
        public string displayName;
        public string discription;
        public enum Kind { Equipment, Consumpution,Ever }
        public Kind kind;
        public Sprite sprite;
    }

    [CreateAssetMenu(menuName ="Items/Consumption",fileName ="Potion")]
    public class HealPotion : Consumption
    {
        public int hpHealPoint;
        public int spHealPoint;

        public override void OnUse(Entity user)
        {
            if (user is Animal a)
            {
                a.HealHp(hpHealPoint);
                a.HealSp(spHealPoint);
            }
            else
            {
                user.SaySth("Can't Use it");
            }
        }
    }

    public abstract class Consumption : Item
    {
        public bool Stackable;
        public int maxStack;

        public abstract void OnUse(Entity user);
    }

    [CreateAssetMenu(menuName ="Items/Weapon",fileName ="Weapon")]
    public class Equipment : Item
    {
        public enum Slot { Weapon,Armor}
        public Slot slot;
        public enum Affix { A,b,c,d,e,f,g}
    }


    public enum Element { Fire,Ice, Arc,None}


}
