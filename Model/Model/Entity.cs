using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Model
{
    public abstract class Entity:MonoBehaviour
    {
        public abstract Action<Entity>[] GetReactions(Entity sponser);

        public LinkedList<Buff> buffs;

        public event Action<Entity> ReactStarted;
        public event Action<Entity> ReactCompleted;
        public event Action<Entity> ReactInturrpted;
        public event Action AttrUpdated;

        [SerializeField]
        protected Coroutine curTask;
        private Entity trader;

        private void Start()
        {
        }

        protected void UpdateAttr()
        {
            AttrUpdated?.Invoke();
        }

        public enum Status { Idle,Reacting,Stun}
        public Status status=Status.Idle;

        public virtual string Info {get=>"test"; }

        public virtual void SaySth(string str)
        {
            Debug.Log(str);
        }

        public virtual void InterruptReact()
        {
            if (curTask != null)
            {
                StopCoroutine(curTask);
                ReactInturrpted?.Invoke(this);

                trader.InterruptHandler();
                InterruptHandler();
            }
        }

        private void InterruptHandler()
        {
            
            Debug.Log("Interrupted");
            curTask = null;
            trader = null;
            status = Status.Idle;
        }
        
        protected virtual void CompleteReact(Entity entity)
        {
            curTask = null;
            trader = null;
            ReactCompleted?.Invoke(this);
            status = Status.Idle;
        }

        protected virtual void StartReact(Coroutine coro,Entity trader)
        {
            ReactStarted?.Invoke(this);
            curTask = coro;
            this.trader = trader;
            status = Status.Reacting;
        }

        protected IEnumerator wrapper(IEnumerator ie,Entity user)
        {
            var coro = user.StartCoroutine(ie);
            user.StartReact(coro,this);
            StartReact(coro,user);

            yield return coro;

            user.CompleteReact(user);
            this.CompleteReact(this);

        }

        protected void BeginReact(IEnumerator ie, Entity user)
        {
            if ((status == Status.Idle) && (user.status == Status.Idle))
            {
                var coro = user.StartCoroutine(wrapper(ie, user));

            }
        }
    }
}
