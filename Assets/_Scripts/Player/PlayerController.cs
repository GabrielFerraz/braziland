using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D body;
    public float moveSpeed = 3f;

    public Transform characterEye;
    private Animator _anim;

    float moveX, moveY;
    Vector2 moveDir;

    public LayerMask interactLayer;
    public float interactableSight; // how close sight is recognized for interaction. 

    private Collider2D sightedObjectCollider;
    public GameEvent @InteractDone;

    Ray2D interactRay = new();
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        InteractDone.OnRaise.AddListener((x) => IsInteracting = false);
        _anim.SetFloat("LastVer", 1); // start facing up. 
        IsInteracting = false; 
    }
    private void OnDestroy()
    {
        InteractDone.OnRaise.RemoveAllListeners();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsInteracting) return; // don't move if interacting. 

        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");

        moveDir = new Vector2(moveX, moveY);

        _anim.SetFloat("Horizontal", moveX);
        _anim.SetFloat("Vertical", moveY);
        _anim.SetFloat("MoveSpeed", moveDir.sqrMagnitude); // the squared length of the vector. 

        if (moveX == 1 || moveX == -1 || moveY == 1 || moveY == -1)
        {
            _anim.SetFloat("LastHor", moveX);
            _anim.SetFloat("LastVer", moveY);

            //Debug.Log("Last Horizontal is: " + _anim.GetFloat("LastHor"));
            //Debug.Log("Last Vertical is: " + _anim.GetFloat("LastVer"));
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            InteractWithObject();
        }
    }

    public bool IsInteracting { get; set; }
    public void InteractWithObject()
    {
        if (!IsInteracting)
            sightedObjectCollider.GetComponent<IInteractable>()?.Interact();
    }

    private void FixedUpdate()
    {
        interactRay.origin = (Vector2)characterEye.position;
        body.velocity = moveSpeed * moveDir;

        if (moveX > 0 && moveX > moveY)
        {
            interactRay.direction = Vector2.right;
        }
        if (moveY > 0 && moveY > moveX)
        {
            interactRay.direction = Vector2.up;
        }
        if (moveX < 0 && Mathf.Abs(moveX) > Mathf.Abs(moveY))
        {
            interactRay.direction = Vector2.left;
        }
        if (moveY < 0 && Mathf.Abs(moveY) > Mathf.Abs(moveX))
        {
            interactRay.direction = Vector2.down;
        }

        RaycastHit2D sighted = Physics2D.Raycast(interactRay.origin, interactRay.direction, interactableSight, interactLayer);

        Debug.DrawRay(interactRay.origin, interactRay.direction, Color.red);
        if (sighted)
        {
            sightedObjectCollider = sighted.collider;
            Debug.Log("Looking at: " + sighted.collider.name);
        }
    }
}