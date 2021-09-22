using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public ThinkingController thinkingController;
    public LayerMask groundLayer;

    private Rigidbody2D body;
    private Animator anim;
    private SpriteRenderer playerSprite;
    private int direction;
    private BoxCollider2D boxCollider;

    public float speed;
    private bool movable;
    

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        playerSprite = GetComponentInChildren<SpriteRenderer>();
        movable = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (thinkingController.getThinkingState())
        {
            anim.SetBool("run", false);
            anim.SetBool("grounded", isGrounded());
            return;
        }

        float horizontalInput = 0;

        if (movable)
            horizontalInput = Input.GetAxis("Horizontal");

        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        if (horizontalInput > 0.01f)
            direction = 1;
        else if (horizontalInput < -0.01f)
            direction = -1;

        playerSprite.flipX = direction == 1;

        //if (Input.GetKey(KeyCode.Space) & isGrounded() & movable)
        //Jump();

        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, speed);
        anim.SetTrigger("jump");
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    public void toggleMovable(bool canMove)
    {
        movable = canMove;
    }

}
