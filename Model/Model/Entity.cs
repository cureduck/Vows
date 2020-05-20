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
        
        
        public event Action<Entity> CastStarted;
        public event Action<Entity> CastCompleted;
        public event Action<Entity> CastInterrupted;
        public event Action AttrUpdated;
        public event Action<float> ProgressUpdated;

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
            CastInterrupted?.Invoke(this);
        }

        internal void BroadcastComplete()
        {
            CastCompleted?.Invoke(this);
        }

        internal void BroadCastProgress(float progress)
        {
            ProgressUpdated?.Invoke(progress);
        }
    }
}
