using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Tilemaps;
using Utils;

namespace Manager
{
    public class MapManager : SerializedMonoBehaviour
    {

        public Tilemap tilemap;
        
        public TileBase Common;
        public TileBase Opened;
        
        public enum SquareType
        {
            Common,Planted,Opened
        }

        public struct Square
        {
            public SquareType Content;
        }
        
        [HideInInspector]
        public Square[][] map;

        public int Length;

        private void Start()
        {
            tilemap.size=new Vector3Int(Length,Length,0);

            map=new Square[Length][];
            for (var i = 0; i < Length; i++)
            {
                map[i]=new Square[Length];
                
                for (var j = 0; j < Length; j++)
                {
                    //tilemap.SetTile(new Vector3Int(i,j,0), Common );
                }
            }
            
            tilemap.FloodFill(Vector3Int.zero, Common);
            
            
            tilemap.SetTile(new Vector3Int(3,3,0), Opened );
            tilemap.SetTile(new Vector3Int(3,4,0), Opened );
        }
        
        
    }
}
