using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PlayerMovement : MonoBehaviour
{
    public ThinkingController thinkingController;
    
    private Rigidbody2D body;
    private Animator anim;
    private SpriteRenderer playerSprite;
    private int direction;
    private BoxCollider2D boxCollider;
    private static int CSrunning;
    private static int CSrunDirection;
    private static float CSmoveSpeed;
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
	CSrunning = 0;
        CSrunDirection = 0;
    }

    void Update()
    {
	AudioController.walkingPlayer = anim.GetBool("run");
        if (CSrunning >0)
	{
	    anim.SetBool("run", true);
	    body.velocity = new Vector2(CSrunDirection * CSmoveSpeed, body.velocity.y);
	    --CSrunning;
	    return;
	}
	
	if (thinkingController.getThinkingState())
	{
	    anim.SetBool("run", false);
	    return;
	}
	
	float horizontalInput = 0;
	if (movable )
	    horizontalInput = Input.GetAxis("Horizontal");
	
	body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
	if (horizontalInput > 0.01f)
	    direction = 1;
	
	else if (horizontalInput < -0.01f)
	    direction = -1;
	
	playerSprite.flipX = direction == 1;
	anim.SetBool("run", horizontalInput != 0);
	
    }
    public static void CutSceneRun(float xoffset,float movespeed)
    {
	if( xoffset < 0.0f)
	    CSrunDirection = -1;
	else
	    CSrunDirection = 1;
	CSmoveSpeed = movespeed;
	
	CSrunning = Math.Abs((int) Mathf.Ceil( (float) (xoffset / movespeed )));
    }

    public static void toggleMovable(bool canMove)
    {
        movable = canMove;
    }

    public static void toggleThinkable(bool canAct)
    {
        thinkable = canAct;
    }
    public void SetThought(string thought, string solution,  UnityEvent solved ,UnityEvent notSolved)
    {
	Debug.Log("SetThought Playermovement");
    	thinkingController.SetThought(thought.ToUpper(),solution.ToUpper(),solved,notSolved);
    }
}
