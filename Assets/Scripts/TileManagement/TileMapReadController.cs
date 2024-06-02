using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapReadController : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;
    [SerializeField] List<TileData> tileDatas;
    [SerializeField] MarkerManager markerManager;


    Dictionary<TileBase, TileData> dataFromTiles;


    private void Start()
    {
        dataFromTiles = new Dictionary<TileBase, TileData>();

        foreach (TileData tileData in tileDatas)
        {
            foreach (TileBase tile in tileData.tiles)
            {
                dataFromTiles.Add(tile, tileData);  

            }
        }
    }

    //private void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {

    //        GetTileBase(Input.mousePosition);
    //    }
    //}

    public Vector3Int GetGridPosition(Vector2 position, bool mousePosition)
    {
        Vector3 worldPosition;
        if (mousePosition)
        {
            worldPosition = Camera.main.ScreenToWorldPoint(position);

        }
        else
        {
            worldPosition = position;
        }
        Vector3Int gridPosition = tilemap.WorldToCell(worldPosition);

        return gridPosition;

    }
    public TileBase GetTileBase(Vector3Int gridPosition, bool mousePosition = false)
    {
       

        TileBase tile = tilemap.GetTile(gridPosition);

        return tile;
    }
    public TileData GetTileData(TileBase tilebase)
    {

        return dataFromTiles[tilebase];
    }
}
