using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Model
{
    public abstract class Entity:MonoBehaviour
    {
        public abstract Action<Entity>[] actions { get; }

        public LinkedList<Buff> buffs;

        public event Action<Entity> ReactCompleted;
        public event Action<Entity> ReactInturrpted;
        public event Action AttrUpdated;

        protected void UpdateAttr()
        {
            AttrUpdated?.Invoke();
        }

        public abstract Tuple<string, string>[] Status { get; }
        public virtual string Info {get=>"test"; }
    }
}
