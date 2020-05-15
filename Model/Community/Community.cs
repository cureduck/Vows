using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
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

        public Class[] classes;

        public event Action StatusChanged; 
        

        public Community(Class[] classes)
        {
            this.classes = classes;
        }

        /// <summary>
        /// 默认为顺位调整制度，即为上位角色过世，由下位第一角色继任
        /// </summary>
        /// <returns></returns>
        public virtual void PostionAdjust()
        {
            for (var i = 0; i <classes.Length ; i++)
            {
                if (classes[i].actors.Count >= classes[i].capacity.start) continue;
                Debug.Log(classes[i + 1].actors.First.Value.name + "继任了");

                classes[i].actors.AddLast(classes[i + 1].actors.First.Value);
                classes[i + 1].actors.RemoveFirst();
            }
        }

        public override string ToString()
        {
            return classes.Aggregate("", (current, roles) => current + (roles.name + ":"));
        }

        public void Remove(Animal person)
        {
            foreach (var t in classes)
            {
                t.actors.Remove(person);
            }

            PostionAdjust();
        }

        public bool TryAdd(Animal person,int rank)
        {
            if (classes[rank].actors.Count< classes[rank].capacity.end)
            {
                classes[rank].actors.AddLast(person);
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
