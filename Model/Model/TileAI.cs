using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BehaviorDesigner.Runtime.Tasks.Basic.UnityGameObject;
using Model.Buff;
using Model.Community;
using Sirenix.OdinInspector;
using UnityEngine;
using Utils;

namespace Model
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class TileAI:SerializedMonoBehaviour
    {
        public float velocity;
        private Rigidbody2D _rb;
        public Vector2Int position => Tools.Vec2Int(_rb.position);


        public Vector2Int test;
        public Vector2Int dest;

        public Map map;
        [Button]
        
        public void FindPath()
        {
            FindPath(map,dest);
        }
        
        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }



        public void FindPath(Map map,Vector2Int des)
        {
            
            Debug.Log(1);
            var current=new Node(position);

            Dictionary<Node, int> openList = new Dictionary<Node, int>();
            openList.Add(current,current.fn);
            HashSet<Node> closeList=new HashSet<Node>();
            
            while (openList.Count>0 && (current.coordinate != des))
            {
                
                Debug.Log(current.coordinate);
                
                //取出openlist中预测最短路径节点，准备对其进行扩展
                var m= openList.Values.Min();
                current= openList.First((pair => pair.Value == m)).Key;
                openList.Remove(current);
                //将扩展节点加入openlist
                foreach (var neighbour in current.Neighbours(des))
                {
                    if (map.CanPass(this,neighbour.coordinate)&& !closeList.Contains(neighbour))
                    {
                        if (!openList.ContainsKey(neighbour))
                        {
                            openList.Add(neighbour,neighbour.fn);
                        }
                    }
                }

                closeList.Add(current);
            }
            
            if (current.coordinate==des)
            {
                while (current.privious!=null)
                {
                    current = current.privious;
                }
            }
            
            
        }
        
        
        public bool hasReached { get; private set; }
        
        
        
        internal class Node
        {
            internal readonly Vector2Int coordinate;

            internal Node privious;

            internal int fn;
            internal int gn;
            internal int hn;
            

            internal Node(Vector2Int pos)
            {
                coordinate = pos;
            }

            internal Node(Vector2Int pos, Node privious,int gn,int hn)
            {
                coordinate = pos;
                this.privious = privious;
                this.gn = gn;
                this.hn = hn;
                this.fn = gn + hn;
            }

            public override int GetHashCode()
            {
                return coordinate.GetHashCode();
            }

            public override bool Equals(object obj)
            {
                return obj is Node that && this.coordinate == that.coordinate;
            }

            public Node[] Neighbours(Vector2Int des)
            {
                var tmp = new Node[4];
                for (int i = 0; i < 4; i++)
                {
                    tmp[i] = new Node(pos: (coordinate+Dir[i]),this,ManhDistance(des,coordinate+Dir[i]),hn+1);
                }
                return tmp;
            }

            private readonly Vector2Int[] Dir = new[]
            {
                Vector2Int.down,
                Vector2Int.up,
                Vector2Int.left,
                Vector2Int.right,
            };

            public int ManhDistance(Vector2Int vec1, Vector2Int vec2)
            {
                return Math.Abs((vec1 - vec2).x) + Math.Abs((vec1 - vec2).y);
            }
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
