using System;
using UnityEngine;
using UnityEngine.EventSystems;


public class ClosePanel : MonoBehaviour, IPointerClickHandler {
  public ToolPanel parent;

  public void Start() {
  }

  public void OnPointerClick(PointerEventData eventData) {
    gameObject.transform.parent.gameObject.SetActive(false);
    Debug.Log("Clicked");
  }
}