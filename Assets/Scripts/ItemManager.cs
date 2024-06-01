using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class ItemManager : MonoBehaviour
{
    public Item[] items;

    private Dictionary<string, Item> NameToItemDict =
    new Dictionary<string, Item>();

    private void Awake()
    {

        foreach (Item item in items)
        {
            AddItem(item);
        }
    }
    private void AddItem(Item item)
    {

        if (!NameToItemDict.ContainsKey(item.data.itemName))
            NameToItemDict.Add(item.data.itemName, item);

    }

    public Item GetItemByName(string key)
    { 

            if (NameToItemDict.ContainsKey(key))
                return NameToItemDict[key];
            return null;
    }


}
