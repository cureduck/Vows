using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Buff;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Model
{
    public abstract class Entity:SerializedMonoBehaviour
    {
        public abstract Action<Entity>[] GetReactions(Entity sponser);
        
        
        public event Action<Entity> ReactStarted;
        public event Action<Entity> ReactCompleted;
        public event Action<Entity> ReactInturrpted;
        public event Action AttrUpdated;
        public event Action<float> ProgressUpdated;

        [SerializeField]
        protected Coroutine realTask;
        private Entity _trader;
        public List<Buff.Buff> buffs;

        public ExclusiveBuff task;

        private void Start()
        {
        }

        protected void UpdateAttr()
        {
            AttrUpdated?.Invoke();
        }

        public enum Status { Idle, Reacting, Stun}
        [SerializeField] private Status _status = Status.Idle;
        public virtual Status status { get => _status; set => _status = value; }
        
        [Button]
        public virtual void InterruptReact()
        {
            ReactInturrpted?.Invoke(this);
        }

        internal void BroadcastComplete()
        {
            ReactCompleted?.Invoke(this);
        }
        
    }
}
