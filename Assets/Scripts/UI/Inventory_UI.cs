using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.tvOS;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Inventory_UI : MonoBehaviour
{
    public GameObject inventoryPanel;

    public Player player;

    public List<Slot_UI> slots = new List<Slot_UI>();

    [SerializeField] private Canvas canvas;

    private Slot_UI draggedSlot;

    private Image draggedIcon;
    private bool dragSingle;

    private void Awake()
    {
        canvas = FindObjectOfType<Canvas>();
        //Item item = GameManager.instance.itemManager.GetItemByName("Tomato");
        //playerTransform.inventory.Add(item);

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventory();
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            dragSingle = true;
        }
        else
        {
            dragSingle = false;
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
        if (slots.Count == 
            player.inventory.slots.Count)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                if (i < player.inventory.slots.Count)
                {
                    if (!string.IsNullOrEmpty(player.inventory.slots[i].itemName))
                    {
                        slots[i].SetItem(player.inventory.slots[i]);
                    }
                    else
                    {
                        slots[i].SetEmpty();
                    }
                }
                else
                {
                    // Handle the case where the playerTransform's inventory has fewer slots than expected.
                    slots[i].SetEmpty();
                }
            }
        }
    }

    public void Remove()
    {
        if (player != null && player.inventory != null && player.inventory.slots != null)
        {
            
            Item itemToDrop = GameManager.instance.itemManager.GetItemByName(player.inventory.slots[draggedSlot.slotID].itemName);

            if (itemToDrop != null)
            {
                if (dragSingle)
                {
                    player.DropItem(itemToDrop);
                    player.inventory.Remove(draggedSlot.slotID);

                }
                else
                {
                    player.DropItem(itemToDrop, player.inventory.slots[draggedSlot.slotID].count);
                    player.inventory.Remove(draggedSlot.slotID, player.inventory.slots[draggedSlot.slotID].count);

                }
                Refresh();
            }
        }
        draggedSlot = null;


    }
    public void SlotBeginDrag(Slot_UI slot)
    {
        draggedSlot = slot;
        draggedIcon = Instantiate(draggedSlot.itemIcon);
        draggedIcon.transform.SetParent(canvas.transform);
        draggedIcon.raycastTarget = false;
        draggedIcon.rectTransform.sizeDelta = new Vector2(0.2f,0.2f);
        MoveToMousePosition(draggedIcon.gameObject);
        Debug.Log("Start Drag: " + draggedSlot.name);
    }
    public void SlotDrag()
    {
        MoveToMousePosition(draggedIcon.gameObject);

        Debug.Log("Dragging: " + draggedSlot.name);
    }
    public void SlotEndDrag() 
    {
        Destroy(draggedIcon.gameObject);

        draggedIcon = null;


        Debug.Log("Done Dragging: " + draggedSlot.name);
        Remove();

    }
    public void SlotDrop(Slot_UI slot)
    {
        Debug.Log("Dropped " + draggedSlot.name + " on " + slot.name);
    }
    private void MoveToMousePosition(GameObject toMove)
    {
        if (canvas != null)
        {
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform,
                Input.mousePosition, null, out position);

            toMove.transform.position = canvas.transform.TransformPoint(position);
        }
    }
}
