using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private RectTransform rect;
    private Image img;
    
    // Start is called before the first frame update
    void Start() {
        rect = GetComponent<RectTransform>();
        img = GetComponent<Image>();
    }

    public void OnDrag(PointerEventData eventData) {
        rect.anchoredPosition += eventData.delta;
    }

    public void OnBeginDrag(PointerEventData eventData) {
        img.color = new Color(255, 255, 255, 170);
    }

    public void OnEndDrag(PointerEventData eventData) {
        img.color = new Color(255, 255, 255, 255);
    }
}
