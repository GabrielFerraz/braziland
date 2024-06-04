using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D body;
    public float moveSpeed = 3f;

    float moveX, moveY;
    Vector2 moveDir;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxis("Vertical");

        moveDir = new Vector2(moveX, moveY);
    }

    private void FixedUpdate()
    {
        body.velocity = moveSpeed * moveDir;
    }
}