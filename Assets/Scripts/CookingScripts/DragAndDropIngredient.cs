using System.Collections;
using System.Collections.Generic;
using CookingScripts;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class DragAndDropIngredient : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private RectTransform rect;
    private Image img;
    private bool canPlace;
    private Vector2 startPosition;
    private ToolPosition triggeredPosition;
    
    public List<string> allowedTools;
    [FormerlySerializedAs("item")] public ItemScriptableObj itemScriptableObj;

    public void Start() {
        rect = GetComponent<RectTransform>();
        img = GetComponent<Image>();
        startPosition = transform.position;
        canPlace = false;
    }

    public void OnDrag(PointerEventData eventData) {
        // rect.anchoredPosition += eventData.delta;
        rect.position = eventData.position;
    }

    public void OnBeginDrag(PointerEventData eventData) {
        img.color = new Color(255, 255, 255, 170);
    }

    public void OnEndDrag(PointerEventData eventData) {
        if (canPlace) {
            Debug.Log("canPlace");
            triggeredPosition.AddIngredient(itemScriptableObj.number);
            gameObject.SetActive(false);
        } else {
            rect.position = startPosition;
        }
    }

    public void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Ing Trigger");
        var toolPosition = other.GetComponent<ToolPosition>();
        string toolName = toolPosition.activeTool;
        if (allowedTools.Contains(toolName)) {
            Debug.Log("can");
            canPlace = true;
            triggeredPosition = toolPosition;
        }
    }

    public void OnTriggerExit2D(Collider2D other) {
        Debug.Log("Ing Trigger Exit");
    }
}
