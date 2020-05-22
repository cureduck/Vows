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
        
        [ShowInInspector]
        public ExclusiveBuff task;

        private void Start()
        {
        }

        protected void UpdateAttr()
        {
            AttrUpdated?.Invoke();
        }

        public enum Status { Idle, Reacting, Stun}
        [ShowInInspector]
        public Status status { get; set; }
        
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

    public abstract class Property : Entity
    {
        public ISubject owner;
        
        [ShowInInspector]
        public int PermissionLevel { get; set; }
    }
}
