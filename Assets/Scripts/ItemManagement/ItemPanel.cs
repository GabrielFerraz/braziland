using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class ItemPanel : MonoBehaviour
{
    [SerializeField] Inventory inventory;
    [SerializeField] List<Slot_UI> slots;

    //[SerializeField] List<InventoryButton> buttons;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        //SetIndex();
        //Show();
    }

    //private void OnEnable()
    //{
    //    Show();

    //}

    //private void SetIndex()
    //{
    //    for (int i = 0; i < inventory.slots.Count && i < slots.Count; i++)
    //        buttons[i].SetIndex(i);
    //}
    //public void Show()
    //{
    //    for (int i = 0; i < inventory.slots.Count && i < slots.Count; i++)

    //        if (inventory.slots[i].itemName == "")

    //            slots[i].butt .Clean();
    //        else
    //            buttons[i].Set(inventory.slots[i]);
    //}
    public virtual void OnClick(int id)
    {

    }
}
