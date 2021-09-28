using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public ThinkingController thinkingController;

    private Rigidbody2D body;
    private Animator anim;
    private SpriteRenderer playerSprite;
    private int direction;
    private BoxCollider2D boxCollider;

    public float speed;
    public bool movable;
    public bool thinkable;


    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        playerSprite = GetComponentInChildren<SpriteRenderer>();
        movable = false;
        thinkable = false;
    }

    void Update()
    {
        if (CutSceneManager.getCurrentCutScene() == 6)
            toggleMovable(false);

        AudioController.walkingPlayer = anim.GetBool("run");

        if (thinkingController.getThinkingState())
        {
            anim.SetBool("run", false);
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

        anim.SetBool("run", horizontalInput != 0);
    }

    public void toggleMovable(bool canMove)
    {
        movable = canMove;
    }

    public void toggleThinkable(bool canAct)
    {
        thinkable = canAct;
    }
}
