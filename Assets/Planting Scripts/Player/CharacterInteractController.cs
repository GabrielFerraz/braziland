using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CharacterInteractController : MonoBehaviour
{
    Player player;
    Movement movement;
    Rigidbody2D rgbd2d;
    [SerializeField] float offsetDistance = 1f;
    [SerializeField] float sizeOfInteractableArea = 1.2f;
    Character character;
    [SerializeField] HighlightController highlightController;

    private void Awake()
    {
        player = GetComponent<Player>();
        rgbd2d = GetComponent<Rigidbody2D>();
        movement = GetComponent<Movement>();
    }

    private void Update()
    {
        Check();
        if (Input.GetMouseButtonDown(1))
            Interact();
    }

    private void Check()
    {
        Vector2 position = rgbd2d.position + (Vector2) movement.direction * offsetDistance;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, sizeOfInteractableArea);
        
       
        
        foreach (Collider2D c in colliders)
        {
            Interactable hit = c.GetComponent<Interactable>();
            if (hit != null)
            {
                highlightController.Highlight(hit.gameObject);
                return;
            }
        }
            highlightController.Hide();
    }

    private void Interact()
    {
        Vector2 position = rgbd2d.position + (Vector2) movement.direction * offsetDistance;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, sizeOfInteractableArea);

        foreach (Collider2D c in colliders)
        {
            Interactable hit = c.GetComponent<Interactable>();
            if (hit != null)
            {
                hit.Interact(character);
                break;
            }
        } 
    }
}
