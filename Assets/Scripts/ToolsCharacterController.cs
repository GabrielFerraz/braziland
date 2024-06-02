using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.Tilemaps;

public class ToolsCharacterController : MonoBehaviour
{
    Rigidbody2D rgbd2d;
    [SerializeField] float offsetDistance = 1f;
    [SerializeField] float sizeOfInteractableArea = 1.2f;
    [SerializeField] TileMapReadController tileMapReadController;
    [SerializeField] MarkerManager markerManager;
    [SerializeField] float maxDistance=1.5f;
    [SerializeField] CropsManager cropsManager;
    [SerializeField] TileData plowableTiles;

    Vector3Int selectedTilePosition;
    bool selectable;
    private void Awake() 
    {
        rgbd2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        SelectTile();
        CanSelectCheck();   

        Marker();
        if (Input.GetMouseButtonDown(0))
        {
            if(UseToolWorld()) return;
            UseToolGrid();
        }
    }

    private void SelectTile()
    {
        selectedTilePosition = tileMapReadController.GetGridPosition(Input.mousePosition, true);
    }
    void CanSelectCheck()
    {
        Vector2 characterPosition = transform.position;
        Vector2 cameraPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        selectable = Vector2.Distance(characterPosition, cameraPosition) < maxDistance;
        markerManager.Show(selectable);
    }
    private void Marker()
    {

        markerManager.markedCellPosition = selectedTilePosition;
    }

    private bool UseToolWorld()
    {
        Vector2 position = rgbd2d.position;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, sizeOfInteractableArea);

        foreach (Collider2D c in colliders)
        {
            //ToolHit hit = c.GetComponent<ToolHit>();
            //if (hit != null)
            //{
            //    hit.Hit();
            //    break;
            //}
            //return true;
        }
        return false;
    }
    private void UseToolGrid()
    {
        if (selectable)
        {
            
            //TileBase tileBase = tileMapReadController.GetTileBase(selectedTilePosition);
            //TileData tileData = tileMapReadController.GetTileData(tileBase);
            //if (tileData != plowableTiles){return;}
            if (cropsManager.Check(selectedTilePosition))
            {
                cropsManager.Seed(selectedTilePosition);
            }
            else
            {
                cropsManager.Plow(selectedTilePosition);
            }
        }
    }
}
