using System;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks.Basic.UnityVector3;
using Model.Items;
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
            public bool Equals(Square other)
            {
                return Ground == other.Ground;
            }

            public override bool Equals(object obj)
            {
                return obj is Square other && Equals(other);
            }

            public override int GetHashCode()
            {
                return (int) Ground;
            }

            public SquareType Ground;

            public static bool operator ==(Square s1, Square s2)
            {
                return true;
            }

            public static bool operator !=(Square s1, Square s2)
            {
                return false;
            }
        }
        
        [HideInInspector]
        public Square[,] map;

        public int Length;

        private void Start()
        {
            tilemap.size=new Vector3Int(Length,Length,0);

            map=new Square[Length,Length];
            
            tilemap.FloodFill(Vector3Int.zero, Common);
            
            
            tilemap.SetTile(new Vector3Int(3,3,0), Opened );
            tilemap.SetTile(new Vector3Int(3,4,0), Opened );
        }
        
        
    }
}
