using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEditor;

namespace Model
{
    [Serializable]
    public class Class
    {
        public string name;
        [NonSerialized] private Group _group;
        public int rank;

        public RangeInt capacity;
        public LinkedList<Animal> actors;

        [ShowInInspector]
        public int count=> actors?.Count ?? 0;

        public void TakeNew(Animal animal)
        {
            animal.Death += _group.Remove;
            actors.AddLast(animal);
        }

        public Class()
        {
            actors=new LinkedList<Animal>();
        }


        /// <summary>
        /// 把对象引荐向目标层级
        /// </summary>
        /// <param name="animal"></param>
        /// <param name="rank"></param>
        /// <returns></returns>
        public bool Referral(Animal animal,int rank)
        {
            if (this.rank < rank)
            {
                return _group.TryAdd(animal, rank);
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            return name + ":" + actors.Count;
        }
    }
    
}