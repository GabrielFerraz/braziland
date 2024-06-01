using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CookingScripts {
  public class ToolPosition : MonoBehaviour {

    private string activeTool = "";
    private DragAndDropTool activeToolObj;
    
    public Image img;
    public List<string> allowedTools;

    private void Start() {
      img = GetComponent<Image>();
    } 

    public void SetDraggedTool(DragAndDropTool dragged) {
      gameObject.SetActive(true);
      activeToolObj = dragged;
      img.sprite = dragged.GetComponent<Image>().sprite;
      img.color = new Color(255, 255, 255, 0.5f);
    }

    public void SetActiveTool(string toolName) {
      
      activeTool = toolName;
      img.color = new Color(255, 255, 255, 255);
    }

    public bool CanPlace(string toolName) {
      return allowedTools.Contains(toolName);
    }
  }
}