using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[RequireComponent(typeof(Item))]
public class Collectable : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {

        Player player = collision.GetComponent<Player>();
        
        if (player) 
        {

            Item item = GetComponent<Item>();
            if (item != null)
            {
                player.inventory.Add(item);
                Destroy(this.gameObject);

            }
        }
    }
}