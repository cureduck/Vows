using System;
using System.Collections.Generic;
using System.Linq;
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
        public string name = "00";

        private Status _status;
        public Status status 
        {
            get=>_status;
            set
            {
                _status = value;
                StatusChanged?.Invoke();
            }
        }
        public Class[] Classes { get; set; }

        public event Action StatusChanged; 
        

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
            for (var i = 0; i <Classes.Length ; i++)
            {
                if (Classes[i].actors.Count >= Classes[i].capacity.start) continue;
                Debug.Log(Classes[i + 1].actors.First.Value.Name + "继任了");

                Classes[i].actors.AddLast(Classes[i + 1].actors.First.Value);
                Classes[i + 1].actors.RemoveFirst();
            }
        }

        public override string ToString()
        {
            return Classes.Aggregate("", (current, roles) => current + (roles.className + ":"));
        }

        public void Remove(Animal person)
        {
            foreach (var t in Classes)
            {
                t.actors.Remove(person);
            }

            PostionAdjust();
        }

        public bool TryAdd(Animal person,int rank)
        {
            if (Classes[rank].actors.Count< Classes[rank].capacity.end)
            {
                Classes[rank].actors.AddLast(person);
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
        [NonSerialized] private Community _community;
        public int rank;

        public string className;
        public RangeInt capacity;
        public LinkedList<Animal> actors;

        public void TakeNew(Animal animal)
        {
            animal.Death += _community.Remove;
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
                return _community.TryAdd(animal, rank);
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            return className + ":" + actors.Count;
        }
    }
}
