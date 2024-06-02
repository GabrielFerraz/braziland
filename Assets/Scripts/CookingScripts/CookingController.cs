using System.Collections.Generic;
using UnityEngine;

namespace CookingScripts {
  public class CookingController : MonoBehaviour {
    

    public List<ActiveTool> activeTools;
    public RecipeScriptableObj[] recipes;

    public void AddActiveTool(string toolName, string position) {
      var index = activeTools.FindIndex(t => t.toolName == toolName);
      Debug.Log("index: " + index);
      if (index >= 0) {
        activeTools[index].MovePosition(position);
      } else {
        var component = gameObject.AddComponent<ActiveTool>();
        component.SetInitial(toolName, position, recipes);
        activeTools.Add(component);
      }
    }

    public void RemoveActiveTool(string toolName) {
      var tool = activeTools.Find(t => t.toolName == toolName);
      if (tool) {
        activeTools.Remove(tool);
      }
    }

    public void AddItem(string toolName, int ingredientNumber) {
      var index = activeTools.FindIndex(t => t.toolName == toolName);
      activeTools[index].AddIngredient(ingredientNumber);
    }
  }
}