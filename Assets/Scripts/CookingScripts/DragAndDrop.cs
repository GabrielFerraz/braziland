using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private RectTransform rect;
    private Image img;
    public Image placingPosition;
    private bool canPlace = false;
    private Vector2 startPosition;
    
    // Start is called before the first frame update
    void Start() {
        rect = GetComponent<RectTransform>();
        img = GetComponent<Image>();
        startPosition = transform.position;
    }

    public void OnDrag(PointerEventData eventData) {
        placingPosition.gameObject.SetActive(true);
        rect.anchoredPosition += eventData.delta;
    }

    public void OnBeginDrag(PointerEventData eventData) {
        img.color = new Color(255, 255, 255, 170);
    }

    public void OnEndDrag(PointerEventData eventData) {
        img.color = new Color(255, 255, 255, 255);
        if (canPlace) {
            rect.transform.position = placingPosition.transform.position;
        } else {
            rect.transform.position = startPosition;
        }
        placingPosition.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("trigger");
        if (other.gameObject == placingPosition.gameObject) {
            Debug.Log("Collided with position");
            canPlace = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject == placingPosition.gameObject) {
            Debug.Log("Collided with position");
            canPlace = false;
        }
    }
}
