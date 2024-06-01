using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAndDropIngredient : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private RectTransform rect;
    private Image img;
    private bool canPlace;
    private Vector2 startPosition;
    public List<string> tags;

    public void Start() {
        rect = GetComponent<RectTransform>();
        img = GetComponent<Image>();
        startPosition = transform.position;
        canPlace = false;
    }

    public void OnDrag(PointerEventData eventData) {
        rect.anchoredPosition += eventData.delta;
    }

    public void OnBeginDrag(PointerEventData eventData) {
        img.color = new Color(255, 255, 255, 170);
    }

    public void OnEndDrag(PointerEventData eventData) {
    }

    public void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Ing Trigger");
    }

    public void OnTriggerExit2D(Collider2D other) {
        Debug.Log("Ing Trigger Exit");
    }
}
