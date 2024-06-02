using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_UI : MonoBehaviour
{
    public GameObject inventoryPanel;

    public Player player;

    public List<Slot_UI> slots = new List<Slot_UI>();

    [SerializeField] private Canvas canvas;

    private Slot_UI draggedSlot;

    private Image draggedIcon;

    private void Awake()
    {
        canvas = FindObjectOfType<Canvas>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventory();
        }
    }
    public void ToggleInventory()
    {
        if (!inventoryPanel.activeSelf)
        {
            inventoryPanel.SetActive(true);
            Refresh();
        }
        else
        {
            inventoryPanel.SetActive(false);
        }
    }
    void Refresh()
    {
        if (slots.Count == player.inventory.slots.Count)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                if (player.inventory.slots[i].itemName != "")
                {
                    slots[i].SetItem(player.inventory.slots[i]);
                }
                else
                {
                    slots[i].SetEmpty();
                }
            }
        }
    }
    public void Remove(int slotID)
    {
        if (player != null && player.inventory != null && player.inventory.slots != null)
        {
            
            Item itemToDrop = GameManager.instance.itemManager.GetItemByName(player.inventory.slots[slotID].itemName);

            if (itemToDrop != null)
            {
                player.DropItem(itemToDrop);
                player.inventory.Remove(slotID);
                Refresh();
            }
        }

    }
    public void SlotBeginDrag(Slot_UI slot)
    {
        draggedSlot = slot;
        draggedIcon = Instantiate(draggedSlot.itemIcon);
        Debug.Log("Start Drag: " + draggedSlot.name);
    }
    public void SlotDrag() {

        Debug.Log("Dragging: " + draggedSlot.name);
    }
    public void SlotEndDrag() {

        Debug.Log("Done Dragging: " + draggedSlot.name);
    }
    public void SlotDrop(Slot_UI slot)
    {

        Debug.Log("Dropped " + draggedSlot.name + " on " + slot.name);
    }
}
