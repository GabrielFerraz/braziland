using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static InventoryControl;

public class InventoryItemModel : MonoBehaviour
{
    [NaughtyAttributes.ReadOnly]
    public InventoryItem item;

    public Image itemIcon;
    public TMPro.TextMeshProUGUI itemName;
    public TMPro.TextMeshProUGUI itemQuantity;

    public Transform quantityContainer;

    public GameObject optionsPopup;
    public Button useBtn;
    public Button dropBtn;
    public GameObject useExtraPopup;
    public GameObject dropExtraPopup;


    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            ActivatePopUp();
        });
        useBtn.onClick.AddListener(() =>
        {
        });
        dropBtn.onClick.AddListener(() =>
        {
        });
    }
    // Start is called before the first frame update

    [NaughtyAttributes.Button("Test Item Init")]
    public void InitItem(bool hasQuantity = true)
    {
        itemIcon.sprite = item.m_ItemIcon;
        itemName.text = item.m_ItemName;
        if (hasQuantity)
        {
            quantityContainer.gameObject.SetActive(true);
            itemQuantity.text = item.m_QuantityHeld.ToString();
        }
        else
            quantityContainer.gameObject.SetActive(false);
    }

    public void ActivatePopUp(bool isOn = true)
    {
        optionsPopup.SetActive(isOn);
        if (!isOn)
        {
            useExtraPopup.SetActive(false);
            dropExtraPopup.SetActive(false);
        }
        if (isOn)
        {
            Instance.CloseActivePopup();
            Instance.selectedItem = this;
        }
    }
}
