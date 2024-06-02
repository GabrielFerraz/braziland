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
    private RectTransform meterBar;
    private float t = 0;

    public string toolName = "";
    
    public void SetInitial(string tName, string currentPos, RecipeScriptableObj[] recipeList, RectTransform meter) {
      currentPosition = currentPos;
      recipes = recipeList;
      toolName = tName;
      meterBar = meter;
    }

    private void Update() {
      if (isPreparing) {
        recipeDuration -= Time.deltaTime;
        var size = meterBar.localScale;
        var value = Time.deltaTime * (1/ possibleRecipe.duration);
        size.x += value;
        meterBar.localScale = size;
        Debug.Log("value: " + size);
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
          CheckIngredients();
        }
      }
    }

    public void MovePosition(string pos) {
      currentPosition = pos;
      CheckIngredients();
    }

    private void CheckIngredients() {
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