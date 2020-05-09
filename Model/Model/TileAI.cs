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
        private Rigidbody2D rb;
        public Vector2 position { get => rb.position; }
        public float speed = 3f;
        private Vector2 _destination;
        public Vector2 velocity { get => rb.velocity; }
        private bool _canMove=true;
        public bool canMove { get=>_canMove; set { _canMove = value; if (value) { SetDestination(_destination); } else { rb.velocity = Vector2.zero; } } }

        public event Action Reached;

        public bool hasReached=true;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        public void SetDestination(Vector2 destination)
        {
            hasReached = false;
            _destination = destination;
            var dir= (destination - position).normalized;
            rb.velocity = dir * speed;
        }


        private void Update()
        {
            if (!hasReached)
            {
                if (Vector2.Distance(_destination, position) < 0.01f*speed)
                {
                    rb.velocity = Vector2.zero;
                    transform.position = _destination;
                    Reached?.Invoke();
                    hasReached = true;
                }
            }
        }
    }
}
