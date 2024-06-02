using System;
using System.Collections.Generic;
using UnityEngine;

namespace CookingScripts {
  public class ActiveTool : MonoBehaviour {
    private string currentPosition = "";
    private int ingredientSum = 0;
    private RecipeScriptableObj possibleRecipe;
    private RecipeScriptableObj[] recipes;
    private float recipeDuration;
    private bool isPreparing = false;

    public string toolName = "";
    
    public void SetInitial(string tName, string currentPos, RecipeScriptableObj[] recipeList) {
      currentPosition = currentPos;
      recipes = recipeList;
      toolName = tName;
    }

    private void Update() {
      if (isPreparing) {
        recipeDuration -= Time.deltaTime;
        Debug.Log("preparing");
      }

      if (recipeDuration <= 0 && isPreparing) {
        isPreparing = false;
        Debug.Log("done");
      }
    }

    public void AddIngredient(int ingrNumber) {
      ingredientSum += ingrNumber;
      foreach (var recipe in recipes) {
        if (recipe.sum == ingredientSum) {
          possibleRecipe = recipe;
        }
      }
    }

    public void MovePosition(string pos) {
      currentPosition = pos;
      if (possibleRecipe && currentPosition == possibleRecipe.preparationPosition) {
        StartPreparing();
      }
    }

    private void StartPreparing() {
      recipeDuration = possibleRecipe.duration;
      isPreparing = true;
    }
  }
}