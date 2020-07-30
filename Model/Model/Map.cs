using System.Collections.Generic;
using Model.Community;
using Sirenix.OdinInspector;
using Unity;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Model
{
    public class Map:SerializedMonoBehaviour
    {
        public static Dictionary<int, TileBase> GroundBase;
        public static Dictionary<int, TileBase> TerrainBase;
        public static Dictionary<int, TileBase> BgBase;
        
        

        public int[,] data;

        public Path FindPath(Identity identity)
        {
            return new Path();
        }
    }


    public class Path
    {
        public Vector2Int[] VectorPath;
        
    }
    
}