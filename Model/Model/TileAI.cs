using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BehaviorDesigner.Runtime.Tasks.Basic.UnityGameObject;
using Model.Buff;
using Sirenix.OdinInspector;
using UnityEngine;


namespace Model
{
    [RequireComponent(typeof(Rigidbody2D))]
    class TileAI:SerializedBehaviour
    {
        [ShowInInspector] private Vector2Int _pos;
        [ShowInInspector] private Vector2Int _des;
        
        private Rigidbody2D _rb;
        private Vector2 position => _rb.position;
        public float speed = 3f;
        private Vector2 _destination;
        public Vector2 velocity => _rb.velocity;
        private bool _canMove=true;
        public bool canMove { get=>_canMove; set { _canMove = value; if (value) { SetDestination(_destination); } else { _rb.velocity = Vector2.zero; } } }

        public event Action Reached;

        public bool hasReached=true;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        public void SetDestination(Vector2 destination)
        {
            hasReached = false;
            _destination = destination;
            var dir= (destination - position).normalized;
            _rb.velocity = dir * speed;
        }


        private void Update()
        {
            if (hasReached) return;
            if (!(Vector2.Distance(_destination, position) < 0.03f * speed)) return;
            _rb.velocity = Vector2.zero;
            transform.position = _destination;
            Reached?.Invoke();
            hasReached = true;
        }
    }


    public class Walk : ExclusiveBuff
    {
        private Vector2 _dest;
        private readonly Rigidbody2D _rb;
        public Walk(Entity owner,Vector2 dest, float timeMult = 1) : base(owner, timeMult)
        {
            _dest = dest;
            _rb = owner.GetComponent<Rigidbody2D>();
        }

        public override void TakeOn()
        {
            Owner.status = Entity.Status.Walking;
            base.TakeOn();
        }

        protected override void TakeOff()
        {
            if (Owner.status==Entity.Status.Walking)
            {
                Owner.status = Entity.Status.Idle;
            }
            base.TakeOff();
        }

        protected override void Effect()
        {
            if (Vector2.Distance(_dest,_rb.position)<(1f*_rb.velocity.magnitude))
            {
                _rb.velocity = Vector2.zero;
                _rb.position = _dest;
                TakeOff();
                return;
            }
            var dir = (_dest - _rb.position).normalized;
            _rb.velocity=dir;
            base.Effect();
        }

        public override float baseDuration { get; } = 100f;
    }
    
    
    
}
