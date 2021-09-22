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
    private static bool movable;
    public static bool thinkable;


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
        //FLUMKOD TA BORT NÄR DE SLUTAR VA KUL
        if(CutSceneManager.currentCutscene == 5)
            gameObject.transform.Rotate(1.0f, 1.0f, 1.0f, Space.Self);

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

    public static void toggleMovable(bool canMove)
    {
        movable = canMove;
    }

    public static void toggleThinkable(bool canAct)
    {
        thinkable = canAct;
    }
}
