using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Data", menuName = "Item Data", order =50)]
public class ItemData : ScriptableObject
{
    public string itemName = "Item Name";
    public Sprite icon;
#nullable enable
    public ToolAction? onAction;
    public Crop? crop;
    public ToolAction? onTileMapAction;
    public ToolAction? onItemUsed;


}
