using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    Transform playerTransform;
    Player player;
    [SerializeField] float speed = 5f;
    [SerializeField] float pickUpDistance = 1.5f;
    [SerializeField] float ttl = 10f;
    public Item item;

    public int count = 1;
    private void Awake()
    {

        playerTransform = GameManager.instance.player.transform;
    }
    private void Update()
    {
        ttl -= Time.deltaTime;
        if (ttl < 0) { Destroy(gameObject); }

        float distance = Vector3.Distance(transform.position, playerTransform.position);
        if (distance > pickUpDistance)
            return;

        transform.position = Vector3.MoveTowards(
        transform.position,
        playerTransform.position,
        speed * Time.deltaTime
        );

        if (distance < 0.1f)
        {
            if(player.inventory != null)
            {
                player.inventory.Add(item);
            }
            else
            {
                Debug.Log("No inventory in Game Manager");
            }
            Destroy(gameObject);
        }
    }
    public void Set (Item item , int count)
    {
        this.item = item;
        this.count = count;

        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = item.data.icon;
    }
}
