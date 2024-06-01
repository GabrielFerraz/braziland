using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(menuName = "Inventory", fileName = "Item")]
public class InventoryItem : ScriptableObject
{
    public enum InventoryType
    {
        TEA, TOOLS, INGREDIENT
    }

    [ShowAssetPreview(64, 64)]
    public Sprite m_ItemIcon;
    public string m_ItemName;

    public InventoryType m_Type;

    [HideIf("IsTool")]
    public int m_QuantityHeld;

    #region VALIDATORS 
    public bool IsIngredient() => m_Type == InventoryType.INGREDIENT;
    public bool IsTool() => m_Type == InventoryType.TOOLS;
    public bool IsBake() => m_Type == InventoryType.TEA;
    #endregion
}
