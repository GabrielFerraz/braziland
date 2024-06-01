using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryControl : MonoBehaviour
{
    public static InventoryControl Instance;

    public List<InventoryItem> inventoryItems;

    public GameEvent @IngredientsOpen;
    public GameEvent @ToolsOpen;
    public GameEvent @TeaPlantsOpen;

    List<InventoryItem> teaItems = new();
    List<InventoryItem> toolItems = new();
    List<InventoryItem> ingredientItems = new();

    public Transform itemContent;
    public GameObject inventoryItemPrefab;

    public TMPro.TextMeshProUGUI inventoryTitle;

    [NaughtyAttributes.ReadOnly]
    public InventoryItemModel selectedItem;

    #region MONO
    private void OnDisable()
    {
        ToolsOpen.OnRaise.RemoveAllListeners();
        IngredientsOpen.OnRaise.RemoveAllListeners();
        TeaPlantsOpen.OnRaise.RemoveAllListeners();
    }
    private void OnEnable()
    {
        ToolsOpen.OnRaise.AddListener((x) => LoadTools());
        IngredientsOpen.OnRaise.AddListener((x) => LoadIngredients());
        TeaPlantsOpen.OnRaise.AddListener((x) => LoadTeaPlants());
    }
    private void Awake()
    {
        Instance = this; // destroy on load. 
    }

    private void Start()
    {
        selectedItem = null;
        SortThem();
    }
    #endregion

    public void SortThem()
    {
        foreach (var item in inventoryItems)
        {
            switch (item.m_Type)
            {
                case InventoryItem.InventoryType.TEA:
                    teaItems.Add(item);
                    break;
                case InventoryItem.InventoryType.TOOLS:
                    toolItems.Add(item);
                    break;
                case InventoryItem.InventoryType.INGREDIENT:
                    ingredientItems.Add(item);
                    break;
            }
        }
    }
    public void LoadTeaPlants()
    {
        ClearContent();
        int end = teaItems.Count;
        for (int i = 0; i < end; i++)
        {
            InventoryItemModel _item = Instantiate(inventoryItemPrefab, itemContent).GetComponent<InventoryItemModel>();
            _item.item = teaItems[i];
            _item.InitItem();
        }
        inventoryTitle.SetText("Baking Items");
    }
    public void LoadTools()
    {
        ClearContent();
        int end = toolItems.Count;
        for (int i = 0; i < end; i++)
        {
            InventoryItemModel _item = Instantiate(inventoryItemPrefab, itemContent).GetComponent<InventoryItemModel>();
            _item.item = toolItems[i];
            _item.InitItem(false); // tools don't have quantity...
        }
        inventoryTitle.SetText("Tools");

    }
    public void LoadIngredients()
    {
        ClearContent();
        int end = ingredientItems.Count;
        for (int i = 0; i < end; i++)
        {
            InventoryItemModel _item = Instantiate(inventoryItemPrefab, itemContent).GetComponent<InventoryItemModel>();
            _item.item = ingredientItems[i];
            _item.InitItem();
        }
        inventoryTitle.SetText("Ingredients");
    }
    public void ClearContent()
    {
        for (int i = 0; i < itemContent.childCount; i++)
        {
            Destroy(itemContent.GetChild(i).gameObject);
        }
    }

    public void CloseActivePopup()
    {
        // close the active popup. 
        if (selectedItem == null) return;
        selectedItem.ActivatePopUp(false);
    }
}
