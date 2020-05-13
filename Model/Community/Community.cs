using System;
using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    public abstract class Community
    {
        public enum State
        {
            Building, Completed, Emergency, Dissolved
        }
        public bool Dissolved;
        public Roles[] Classes;

        public abstract bool Collapse();

        /// <summary>
        /// 默认为顺位调整制度，即为上位角色过世，由下位第一角色继任
        /// </summary>
        /// <returns></returns>
        public virtual void PostionAdjust()
        {
            for (int i = 0; i <Classes.Length ; i++)
            {
                if (Classes[i].Actors.Count<Classes[i].Capacity.end)
                {
                    Classes[i].Actors.AddLast(Classes[i + 1].Actors.First);
                    Classes[i + 1].Actors.RemoveFirst();
                }
            }
        }
    }


    public abstract class Roles
    {
        private Community community;
        private int rank;

        public abstract string RoleName { get; }
        public LinkedList<Animal> Actors;
        public abstract RangeInt Capacity { get; }

        public void TakeNew(Animal animal)
        {
            animal.Death += community.PostionAdjust;
            Actors.AddLast(animal);
        }


        /// <summary>
        /// 把对象引荐向目标层级
        /// </summary>
        /// <param name="animal"></param>
        /// <param name="rank"></param>
        /// <returns></returns>
        public bool Referral(Animal animal,int rank)
        {
            if ((this.rank<rank)&&(community.Classes[rank].Capacity.end>Actors.Count))
            {
                community.Classes[rank].TakeNew(animal);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
