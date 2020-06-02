using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace Model
{
    [RequireComponent(typeof(Rigidbody2D))]
    class TileAI:MonoBehaviour
    {
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
}
