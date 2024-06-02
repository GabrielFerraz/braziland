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
    private ToolPosition triggeredPosition;
    
    public Image img;
    // public ToolPosition placingPosition;
    public List<ToolPosition> positions;

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
        foreach (var position in positions) {
            position.SetDraggedTool(this);
        }
        img.color = new Color(255, 255, 255, 100);
        rect.localScale = new Vector3(1, 1, 1);
    }

    public void OnEndDrag(PointerEventData eventData) {
        img.color = new Color(255, 255, 255, 255);
        if (hoveringPlace && triggeredPosition.CanPlace(gameObject.name)) {
            rect.transform.position = triggeredPosition.transform.position;
            rect.localScale = new Vector3(4, 4, 4);
            foreach (var position in positions) {
                string activeName = triggeredPosition.gameObject.name == position.gameObject.name ? gameObject.name : "";
                position.SetActiveTool(activeName);
            }
            
            // gameObject.SetActive(false);
        } else {
            rect.transform.position = startPosition;
            rect.localScale = new Vector3(2, 2, 2);
            triggeredPosition.gameObject.SetActive(false);
            triggeredPosition = null;
            foreach (var position in positions) {
                position.SetActiveTool("");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        foreach (var position in positions) {
            if (other.gameObject == position.gameObject) {
                hoveringPlace = true;
                triggeredPosition = position;
            }
        }
        
    }

    private void OnTriggerExit2D(Collider2D other) {
        hoveringPlace = false;
    }
}
