using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manager;
using UnityEngine;

namespace Model
{
    public class TileAI2 : MonoBehaviour
    {
        private Rigidbody2D _rb;
        private Vector2 position => _rb.position;
        private Vector2 _destination;
        public Vector2 velocity => _rb.velocity;
        private bool _canMove=true;
        public bool hasReached = true;

        private AStar.ANode _path;
        
        public void SetDestination(Vector2Int dest)
        {
            
        }

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (_path!=null)
            {
                
            }
        }
    }
    
    
    class AStar
    {
        /// <summary>
        /// A*的每个节点
        /// </summary>
        public class ANode
        {
            public Vector2Int Vector2Int;
            public ANode parent;
            public int fn, gn, hn;
        }
        private AStar() { }
        public static AStar Instance { get; } = new AStar();
        private MapManager.Square[,] map = null;
        private Dictionary<Vector2Int, ANode> openList = null;
        private HashSet<Vector2Int> closedList = null;
        private Vector2Int dist;
        private MapManager.Square reachableVal;    
 
        /// <summary>
        /// 执行算法
        /// </summary>
        /// <param name="map">二维网格地图，边缘需要用不可达的值填充</param>
        /// <param name="ori">出发点</param>
        /// <param name="dest">目的地</param>
        public ANode Execute(MapManager.Square[,] map, Vector2Int ori, Vector2Int dest, MapManager.Square reachableVal )
        {
            openList = new Dictionary<Vector2Int, ANode>();
            closedList = new HashSet<Vector2Int>();
            this.map = map;
            this.dist = dest;
            this.reachableVal = reachableVal;
            //将初始节点加入到open列表中
            var aNode = new ANode();
            aNode.Vector2Int = ori;
            aNode.parent = null;
            aNode.gn = 0;
            aNode.hn = ManHattan(aNode.Vector2Int, dist);
            aNode.fn = aNode.gn + aNode.hn;
            openList.Add(aNode.Vector2Int, aNode);
 
            while (openList.Count > 0)
            {
                //从open列表中找到f(n)最小的结点
                ANode minFn = FindMinFn(openList);
                Vector2Int Vector2Int = minFn.Vector2Int;
                //判断是否到达终点
                if (Vector2Int.x == dist.x && Vector2Int.y == dist.y) return minFn;
                //去除minFn，加入到closed列表中
                openList.Remove(minFn.Vector2Int);
                closedList.Add(minFn.Vector2Int);
                //将minFn周围的节点加入到open列表中
                AddToOpenList(new Vector2Int(Vector2Int.x - 1, Vector2Int.y), minFn); //左
                AddToOpenList(new Vector2Int(Vector2Int.x + 1, Vector2Int.y), minFn); //右
                AddToOpenList(new Vector2Int(Vector2Int.x, Vector2Int.y - 1), minFn); //上
                AddToOpenList(new Vector2Int(Vector2Int.x, Vector2Int.y + 1), minFn); //下
            }
            return null;
        }
 
        /// <summary>
        /// 输出最短路径
        /// </summary>
        /// <param name="aNode"></param>
        public void DisplayPath(ANode aNode)
        {
            while(aNode != null)
            {
                Console.WriteLine(aNode.Vector2Int.x + "," + aNode.Vector2Int.y);
                aNode = aNode.parent;
            }
        }
 
        /// <summary>
        /// 判断节点是否可达，可达则将节点加入到open列表中
        /// </summary>
        /// <param name="a"></param>
        /// <param name="parent"></param>
        private void AddToOpenList(Vector2Int Vector2Int, ANode parent)
        {
            if(IsReachable(Vector2Int) && !closedList.Contains(Vector2Int))
            {
                ANode aNode = new ANode();
                aNode.Vector2Int = Vector2Int;
                aNode.parent = parent;
                aNode.gn = parent.gn + 1;
                aNode.hn = ManHattan(Vector2Int, dist);
                aNode.fn = aNode.gn + aNode.hn;
                if (openList.ContainsKey(aNode.Vector2Int))
                {
                    if (aNode.fn < openList[aNode.Vector2Int].fn)
                    {
                        openList[aNode.Vector2Int] = aNode;
                    }
                }
                else
                    openList.Add(aNode.Vector2Int, aNode);
            }
        }
 
        /// <summary>
        /// 判定该点是否可达
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        private bool IsReachable(Vector2Int a)
        {
            return map[a.y, a.x] == this.reachableVal;
        }
 
        /// <summary>
        /// 计算两个点之间的曼哈顿距离
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private static int ManHattan(Vector2Int a, Vector2Int b)
        {
            return Math.Abs(a.x - b.x) + Math.Abs(a.y - b.y);
        }
        
        /// <summary>
        /// 从open列表中获取最小f(n)的节点
        /// </summary>
        /// <param name="aNodes"></param>
        /// <returns></returns>
        private ANode FindMinFn(Dictionary<Vector2Int, ANode> aNodes)
        {
            ANode minANode = null;
            foreach(var e in aNodes)
            {
                if(minANode == null || e.Value.fn < minANode.fn)
                {
                    minANode = e.Value;
                }
            }
            return minANode;
        }
    }
}