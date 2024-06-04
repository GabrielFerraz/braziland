﻿using UnityEngine;

namespace CookingScripts {
  [CreateAssetMenu(fileName = "RecipeScriptableObj", menuName = "ScriptableObjects/Recipe", order = 0)]
  public class RecipeScriptableObj : ScriptableObject {
    public int sum;
    public float duration;
    public ItemScriptableObj result;
    public string preparationPosition;
    public string utensilNeeded;
    public AudioClip start;
    public AudioClip loop;
    public AudioClip ending;
  }
}