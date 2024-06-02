using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ToolPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {
    
    private Outline outline; 
    
    public GameObject panel;

    void Start() {
        outline = GetComponent<Outline>();
    }

    public void OnPointerEnter(PointerEventData eventData) {
        outline.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData) {
        outline.enabled = false;
    }

    public void OnPointerClick(PointerEventData eventData) {
        panel.SetActive(true);
    }
}
