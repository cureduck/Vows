using System.Collections.Generic;
using System.Security.AccessControl;
using Model.Community;
using Sirenix.OdinInspector;
using Unity;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;
using System;
using Utils;

namespace Model
{
    public class Map : SerializedMonoBehaviour
    {
        public static Dictionary<int, TileBase> GroundBase;
        public static Dictionary<int, TileBase> TerrainBase;
        public static Dictionary<int, TileBase> BgBase;



        public int[,] GroundData;
        public int[,] TerrainData;
        public int[,] BgData;
        public int[,] BuildingData;
        public int[,] EntityData;

        public static Vector2Int[] Neighbours(Vector2Int vec)
        {
            return new Vector2Int[4]
            {
                new Vector2Int(vec.x, vec.y - 1),
                new Vector2Int(vec.x, vec.y + 1),
                new Vector2Int(vec.x + 1, vec.y),
                new Vector2Int(vec.x - 1, vec.y)
            };
        }

        public bool CanPass(TileAI agent, Vector2Int pos)
        {
            return true;
        }
        
    }
}