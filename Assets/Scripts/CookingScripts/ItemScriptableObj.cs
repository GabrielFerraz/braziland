using UnityEngine;

namespace CookingScripts {
  [CreateAssetMenu(fileName = "ItemScriptableObj", menuName = "ScriptableObjects/CookingItem", order = 0)]
  public class ItemScriptableObj : ScriptableObject {
    public int number;
  }
}