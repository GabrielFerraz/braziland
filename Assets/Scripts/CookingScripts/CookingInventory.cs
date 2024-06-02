using UnityEngine;
using UnityEngine.UI;

namespace CookingScripts {
  public class CookingInventory : MonoBehaviour {

    private int positionIndex = 2;
    
    public GameObject prefab;
    
    
    public void AddItem(Sprite img) {
      var gObj = Instantiate(prefab, new Vector3((160f * positionIndex) , 70f, 0f), Quaternion.identity, transform);
      gObj.GetComponent<Image>().sprite = img;
      positionIndex++;
    }
  }
}