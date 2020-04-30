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
        public bool Reached = false;
        public Vector2 velocity { get => rb.velocity; }

        public event Action Reached;

        private bool hasReached=true;

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
                if (Vector2.Distance(_destination, position) < 0.2f)
                {
                    rb.velocity = Vector2.zero;
                }
                Reached();
                hasReached = true;
            }

        }
    }
}
