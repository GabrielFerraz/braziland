using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/ToolAction/Harvest")]
public class OnTilePickupAction : ToolAction
{
    public override bool OnApplyToTileMap(Vector3Int gridPosition, TileMapReadController tileMapReadController, Item item)
    {

        //tileMapReadController.cropsManager.Pickup(gridPosition);
        return true;
    }
}
