using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    bool facingright=false;

   public Animator animator;

    private void Update()
    {

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, vertical);

        AnimateMovement(direction);

        transform.position += direction * speed * Time.deltaTime;

        if (direction.x == 1 && !facingright) Flip();
        else if (direction.x == -1 && facingright) Flip();

    }

    private void Flip()
    {
        facingright = !facingright;
        transform.Rotate(0,180,0);
    }

    private void AnimateMovement(Vector3 direction)
    {
        if(animator != null) 
        { 
                if(direction.magnitude > 0) 
                {
                    animator.SetBool("IsMoving", true);
                    animator.SetFloat("horizontal", direction.x);
                    animator.SetFloat("vertical", direction.y);

                }
                else 
                {
                animator.SetBool("IsMoving", false);

                }
        }
    }
}
