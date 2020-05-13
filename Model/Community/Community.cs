using System;
using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    [Serializable]
    public class Community
    {
        public enum Status
        {
            Building, Completed, Dissolved
        }

        public Status status;
        public Class[] Classes { get; protected set; }

        public Community(Class[] classes)
        {
            Classes = classes;
        }

        /// <summary>
        /// 默认为顺位调整制度，即为上位角色过世，由下位第一角色继任
        /// </summary>
        /// <returns></returns>
        public virtual void PostionAdjust()
        {
            for (int i = 0; i <Classes.Length ; i++)
            {
                if (Classes[i].Actors.Count<Classes[i].Capacity.start)
                {
                    Debug.Log(Classes[i + 1].Actors.First.Value.Name + "继任了");

                    Classes[i].Actors.AddLast(Classes[i + 1].Actors.First.Value);
                    Classes[i + 1].Actors.RemoveFirst();
                }
            }
        }

        public override string ToString()
        {
            string s = "";
            foreach (var roles in Classes)
            {
                s += roles.ClassName + ":";// + roles.Actors.First.Value.ToString();
            }
            return s;
        }

        public void Remove(Animal person)
        {
            for (int i = 0; i < Classes.Length; i++)
            {
                Classes[i].Actors.Remove(person);
            }

            PostionAdjust();
        }

        public bool TryAdd(Animal person,int rank)
        {
            if (Classes[rank].Actors.Count< Classes[rank].Capacity.end)
            {
                Classes[rank].Actors.AddLast(person);
                return true;
            }
            else
            {
                return false;
            }
        }

    }


    [Serializable]
    public class Class
    {
        [NonSerialized] private Community community;
        public int rank;

        public string ClassName;
        public RangeInt Capacity;
        public LinkedList<Animal> Actors;

        public void TakeNew(Animal animal)
        {
            animal.Death += community.Remove;
            Actors.AddLast(animal);
        }

        public Class()
        {
            
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
                return community.TryAdd(animal, rank);
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            return ClassName + ":" + Actors.Count;
        }
    }
}
