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
    [SerializeField] ToolbarController toolbarController;
    public Player player;
    public Movement movement;

    Vector3Int selectedTilePosition;
    bool selectable;
    private void Awake() 
    {
        player = GetComponent<Player>();
        rgbd2d = GetComponent<Rigidbody2D>();
        movement = GetComponent<Movement>();
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
        Vector2 position = rgbd2d.position + (Vector2)movement.lastDirection * offsetDistance;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, sizeOfInteractableArea);

        foreach (Collider2D c in colliders)
        {
            ToolHit hit = c.GetComponent<ToolHit>();
            if (hit != null)
            {
                //if(hit is TreeCuttable)
                //{
                //    if(tool is TreeCuttable)
                //    CutTree();
                //}

                //if (hit is MineableOre)
                //{
                //    if(tool is pickaxe)
                //    {
                //        MineOre();
                //    }
                //}
                hit.Hit();
                break;
            }
            return true;
        }
        return false;
    }

    private void MineOre()
    {
        throw new NotImplementedException();
    }

    private void CutTree()
    {
        throw new NotImplementedException();
    }

    private void UseToolGrid()
    {
        if (selectable)
        {

            //Item item = GameManager.instance.itemManager.GetItemByName(player.inventory.slots[0].itemName); ;
            //if (item == null) return;
            //if(item.data.onTileMapAction == null ) return;
            //bool complete = item.data.onTileMapAction.OnApplyToTileMap(selectedTilePosition, tileMapReadController,item);
            //TileBase tileBase = tileMapReadController.GetTileBase(selectedTilePosition);
            //TileData tileData = tileMapReadController.GetTileData(tileBase);
            //if (tileData != plowableTiles){return;}
            if (cropsManager.Check(selectedTilePosition))
            {
                //cropsManager.Seed(
                //    selectedTilePosition
                //    ,tileMapReadController
                    //);
            }
            else
            {
                cropsManager.Plow(selectedTilePosition);
            }
        }
    }
}
