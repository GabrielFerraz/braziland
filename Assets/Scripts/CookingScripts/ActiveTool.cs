using System;
using System.Collections;
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
    private string currentAudio = "start";
    private ToolPosition tool;

    public string toolName = "";
    public AudioSource source;
    
    public void SetInitial(string tName, string currentPos, RecipeScriptableObj[] recipeList, RectTransform meter, ToolPosition t) {
      currentPosition = currentPos;
      recipes = recipeList;
      toolName = tName;
      meterBar = meter;
      source = GetComponent<AudioSource>();
      tool = t;
      
    }

    private void Update() {
      if (isPreparing) {
        recipeDuration -= Time.deltaTime;
        var size = meterBar.localScale;
        var value = Time.deltaTime * (1/ possibleRecipe.duration);
        size.x += value;
        meterBar.localScale = size;
        if (!source.isPlaying && currentAudio == "start") {
          currentAudio = "loop";
          source.clip = possibleRecipe.loop;
          source.loop = true;
          source.Play();
        }
        if (recipeDuration <= possibleRecipe.ending.length && currentAudio == "loop") {
          currentAudio = "ending";
          source.Stop();
          source.loop = false;
          source.clip = possibleRecipe.ending;
          source.Play();
        }
      }

      if (recipeDuration <= 0 && isPreparing) {
        isPreparing = false;
        tool.SetReward(possibleRecipe.result.image);
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
      source.clip = possibleRecipe.start;
      source.Play();
    }
  }
}