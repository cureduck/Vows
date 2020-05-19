using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        
        public virtual void InterruptReact()
        {
            if (realTask == null) return;
            StopCoroutine(realTask);
            _trader.InterruptHandler();
            InterruptHandler();
        }

        private void CastEndHandler()
        {

            ReactInturrpted?.Invoke(this);
            realTask = null;
            _trader = null;
            if (status==Status.Reacting)
            {
                status = Status.Idle;
            }
        }

        private void InterruptHandler()
        {
            CastEndHandler();
        }

        protected void InvokeReact(IEnumerator ie,Entity user)
        {
            if ((status != Status.Idle) || (user.status != Status.Idle)) return;
            var coro = user.StartCoroutine(ie);
            user.OnReactStart(coro, this);
            this.OnReactStart(coro, user);
        }

        private void OnReactStart(Coroutine coro, Entity target)
        {
            ReactStarted?.Invoke(this);
            realTask = coro;
            this._trader = target;
            status = Status.Reacting;
        }


        protected internal virtual void OnReactCompleted(Entity target)
        {
            CastEndHandler();
            ReactCompleted?.Invoke(this);
        }
    }
}
