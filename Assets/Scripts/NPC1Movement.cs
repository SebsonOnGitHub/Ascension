using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC1Movement : MonoBehaviour
{
    private Rigidbody2D body;
    private Animator anim;

    public float speed;


    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        CutSceneManager.IncreaseCutScene();
    }

    void Update()
    {
        AudioController.walkingNPC1 = anim.GetBool("run");

        switch (CutSceneManager.getCurrentCutScene())
        {
            case 0:
                break;
            case 1:
                anim.SetBool("run", true);
                body.velocity = new Vector2(-speed, body.velocity.y);
                break;
            case 2:
                anim.SetBool("run", false);
                body.velocity = new Vector2(0, body.velocity.y);
                CutSceneManager.IncreaseCutScene();
                PlayerMovement.toggleThinkable(true);
                break;
            case 3:
                anim.SetBool("run", true);
                body.velocity = new Vector2(speed, body.velocity.y);
                body.GetComponent<SpriteRenderer>().flipX = true;
                MaskController.follow = true;
                break;
            case 4:
                MaskController.follow = false;
                AudioController.walkingNPC1 = false;
                Destroy(gameObject);
                break;
            default:
                Debug.Log("Fault in NPC1Movement");
                break;
        }
    }
}
