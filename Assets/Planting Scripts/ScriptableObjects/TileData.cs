using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


[CreateAssetMenu(fileName = "Tile Tile", menuName = "Tile Data", order = 51)]
public class TileData : ScriptableObject
{
        public List<TileBase> tiles;

        public bool plowable;
}

