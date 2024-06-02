﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CookingScripts {
  public class ToolPosition : MonoBehaviour {

    private DragAndDropTool activeToolObj;
    
    public string activeTool = "";
    public Image img;
    public List<string> allowedTools;
    public CookingController cookingCtrl;

    private void Start() {
      img = GetComponent<Image>();
    } 

    public void SetDraggedTool(DragAndDropTool dragged) {
      if (activeTool == "" || activeTool == dragged.gameObject.name) {
        gameObject.SetActive(true);
        activeToolObj = dragged;
        img.sprite = dragged.GetComponent<Image>().sprite;
        img.color = new Color(255, 255, 255, 0.5f);
        if (activeTool == dragged.gameObject.name) {
          activeTool = "";
          activeToolObj = null;
        }
      }
    }

    public void SetActiveTool(string toolName) {
      if (toolName != "") {
        if (activeTool != "") return;
        activeTool = toolName;
        img.color = new Color(255, 255, 255, 1f);
        cookingCtrl.AddActiveTool(toolName, gameObject.name);
      } else {
        gameObject.SetActive(false);
        cookingCtrl.RemoveActiveTool(toolName);
      }
    }

    public bool CanPlace(string toolName) {
      return activeTool == "" && allowedTools.Contains(toolName);
    }

    public void AddIngredient(int ingredientNumber) {
      cookingCtrl.AddItem(activeTool, ingredientNumber);
    }
  }
}