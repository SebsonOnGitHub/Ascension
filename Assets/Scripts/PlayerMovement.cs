using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.Rendering.Universal;
public class PlayerMovement : MonoBehaviour
{
    public ThinkingController thinkingController;
    public AudioSource wingAudio;
    public AudioSource thoughtAudio;

    private Rigidbody2D body;
    private Animator anim;
    private SpriteRenderer playerSprite;
    private int direction;
    private BoxCollider2D boxCollider;
    private GameObject halo;
    private GameObject candle;
    private static int CSrunning;
    private static int CSrunDirection;
    private static float CSmoveSpeed;
    public float speed;
    public bool movable;
    public bool thinkable;


    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        playerSprite = GetComponentInChildren<SpriteRenderer>();

        foreach (Transform child in transform)
        {
            if (child.name == "Halo")
                halo = child.gameObject;
            if (child.name == "Candle")
                candle = child.gameObject;
        }

        movable = false;
        thinkable = false;
        CSrunning = 0;
        CSrunDirection = 0;

    }

    void FixedUpdate()
    {
        float flyingSpeed = 0.08f;

        switch (CutSceneManager.getCurrentCutScene())
        {
            case 6:
                toggleMovable(false);
                toggleThinkable(false);
                anim.SetBool("wings", true);
                body.gravityScale = 0;
                CutSceneManager.IncreaseCutScene();
                break;
            case 7:
                transform.position = new Vector3(transform.position.x, transform.position.y + flyingSpeed/8, transform.position.z);
                break;
            case 8:
                float triggerX = 47.63f;
                float direction = Mathf.Abs(transform.position.x - triggerX) / (transform.position.x - triggerX);
                transform.position = new Vector3(transform.position.x - direction * flyingSpeed, transform.position.y, transform.position.z);
                if (Mathf.Abs(transform.position.x - triggerX) <= 0.1f)
                    CutSceneManager.IncreaseCutScene();
                break;
            case 9:
                transform.position = new Vector3(transform.position.x, transform.position.y + flyingSpeed, transform.position.z);
                break;
            case 10:
                break;
            case 11:
                transform.position = new Vector3(transform.position.x, transform.position.y + 0.015f, transform.position.z);
                break;
            default:
                break;
        }

        //TODO: Remove legacy code.
        AudioController.walkingPlayer = anim.GetBool("run");

        if (CSrunning > 0)
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

    public static void CutSceneRun(float xoffset, float movespeed) // should not be static.
    {
        if (xoffset < 0.0f)
            CSrunDirection = -1;
        else
            CSrunDirection = 1;
        CSmoveSpeed = movespeed;

        CSrunning = Math.Abs((int)Mathf.Ceil((float)(xoffset / movespeed)));
    }

    public bool isThinking()
    {
        return thinkingController.getThinkingState();
    }
    public void toggleMovable(bool canMove)
    {
        movable = canMove;
    }

    public void toggleThinkable(bool canAct)
    {
        thinkable = canAct;
    }
    public void SetThought(string thought, string solution, UnityEvent solved, UnityEvent notSolved)
    {
        thinkingController.SetThought(thought.ToUpper(), solution.ToUpper(), solved, notSolved);
    }

    public void ToggleHalo()
    {
        anim.SetBool("halo", !anim.GetBool("halo"));
        halo.gameObject.SetActive(!halo.gameObject.activeSelf);
    }

    public void PlayWingSound()
    {
        wingAudio.Play();
    }
    
    public IEnumerator newThought()
    {
        Color color = candle.GetComponent<SpriteRenderer>().color;
        float speed = 0.006f;

        thoughtAudio.Play();

        while (color.a < 1)
        {
            color.a += 1f/255f;
            candle.GetComponent<SpriteRenderer>().color = color;
            yield return new WaitForSecondsRealtime(speed);
        }

        yield return new WaitForSecondsRealtime(0.5f);

        while (color.a > 0)
        {
            color.a -= 1f/255f;
            candle.GetComponent<SpriteRenderer>().color = color;
            yield return new WaitForSecondsRealtime(speed);
        }
    }
}
