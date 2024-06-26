using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class TileManager : MonoBehaviour
{
    [SerializeField] private Tilemap interactableMap;

    [SerializeField] private Tile hiddenInteractableTile;

    [SerializeField] private Tile interactedTile;

    void Start()
    {
        foreach (var position in interactableMap.cellBounds.allPositionsWithin)
        {
            TileBase tile = interactableMap.GetTile(position);

            if (tile != null && tile.name == "interactable_invisible") 
            {
                Debug.Log("so tile name is interactable interesting");
            interactableMap.SetTile(position, hiddenInteractableTile);
            }

        }


    }
    public bool IsInteractable(Vector3Int position)
    {

        TileBase tile = interactableMap.GetTile(position);

        if (tile != null)
        {

            if (tile.name == "interactable_invisible")
            {


                return true;

            }
         

        }
        return false;
    }

    public void SetInteracted(Vector3Int position) 
    {
        interactableMap.SetTile(position,interactedTile);
    }
}
