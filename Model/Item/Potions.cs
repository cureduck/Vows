using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Model
{
    public abstract class Potion:Consumpution
    {
        protected int healPoint;
        public override void Use(Animal user)
        {
            base.Use(user);
            user.HealHp(healPoint);
        }
    }


    public class BigPotion : Potion
    {
        private static readonly string _name = "potion";
        public override string ItemName => _name;
        private static readonly int _maxStack = 10;
        public override int capacity => _maxStack;

        public BigPotion()
        {
            healPoint = 3;
        }
    }
}
