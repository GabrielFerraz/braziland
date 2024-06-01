using System;
using System.Collections;
using System.Collections.Generic;
using CookingScripts;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragAndDropTool : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private RectTransform rect;
    private bool hoveringPlace = false;
    private Vector2 startPosition;
    
    public Image img;
    public ToolPosition placingPosition;
    
    // Start is called before the first frame update
    void Start() {
        rect = GetComponent<RectTransform>();
        img = GetComponent<Image>();
        startPosition = transform.position;
    }

    public void OnDrag(PointerEventData eventData) {
        rect.anchoredPosition += eventData.delta;
    }

    public void OnBeginDrag(PointerEventData eventData) {
        placingPosition.SetDraggedTool(this);
        img.color = new Color(255, 255, 255, 100);
        rect.localScale = new Vector3(1, 1, 1);
    }

    public void OnEndDrag(PointerEventData eventData) {
        img.color = new Color(255, 255, 255, 255);
        if (hoveringPlace && placingPosition.CanPlace(gameObject.name)) {
            rect.transform.position = placingPosition.transform.position;
            rect.localScale = new Vector3(3, 3, 3);
            placingPosition.SetActiveTool(gameObject.name);
            // gameObject.SetActive(false);
        } else {
            rect.transform.position = startPosition;
            rect.localScale = new Vector3(1, 1, 1);
            placingPosition.gameObject.SetActive(false); 
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("trigger");
        if (other.gameObject == placingPosition.gameObject) {
            hoveringPlace = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject == placingPosition.gameObject) {
            hoveringPlace = false;
        }
    }
}
