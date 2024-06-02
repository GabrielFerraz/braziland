using System.Collections.Generic;
using UnityEngine;

namespace CookingScripts {
  public class CookingController : MonoBehaviour {
    

    public List<ActiveTool> activeTools;
    public RecipeScriptableObj[] recipes;

    public void AddActiveTool(string toolName, string position, RectTransform meterBar, ToolPosition t) {
      var index = -1;
      for (int i = 0; i < activeTools.Count; i++) {
        Debug.Log("activeTools[i].toolName: " + activeTools[i].toolName);
        Debug.Log("toolName: " + toolName);
        if (activeTools[i].toolName == toolName) {
          Debug.Log("is same");
          index = i;
        }
      }
      Debug.Log("index: " + index);
      if (index >= 0) {
        activeTools[index].MovePosition(position);
      } else {
        var component = gameObject.AddComponent<ActiveTool>();
        component.SetInitial(toolName, position, recipes, meterBar, t);
        activeTools.Add(component);
      }
    }

    public void RemoveActiveTool(string toolName) {
      var toolIndex = activeTools.FindIndex(t => t.toolName == toolName);
      if (toolIndex >= 0) {
        var tool = activeTools[toolIndex];
        activeTools.RemoveAt(toolIndex);
        Destroy(tool);
      }
    }

    public void AddItem(string toolName, int ingredientNumber) {
      var index = activeTools.FindIndex(t => t.toolName == toolName);
      activeTools[index].AddIngredient(ingredientNumber);
    }
  }
}