using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC1Movement : MonoBehaviour
{
    public float speed;

    public speech_bubble_controller speech_bubble;
    public string Dialogue;
    private bool once;



    // TODO: TA bort LEGACY code

    public PlayerMovement player;

    private Rigidbody2D body;
    private Animator anim;


    void Start()
    {

        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        CutSceneManager.IncreaseCutScene();
	once = true;
    }

    void Update()
    {
        AudioController.walkingNPC1 = anim.GetBool("run");
        switch (CutSceneManager.getCurrentCutScene())
        {
            case 0:
                break;
            case 1: //walk towards player
                anim.SetBool("run", true);
                body.velocity = new Vector2(-speed, body.velocity.y);
                break;
            case 2: // stop walking, spawn speech bubble
                anim.SetBool("run", false);
                body.velocity = new Vector2(0, body.velocity.y);

		CutSceneManager.IncreaseCutScene();
		break;
            case 3: // start talking until player inputs "return"
		if(once)
		{
		    speech_bubble.show (Dialogue);
		    once = false;
		}
		if(Input.GetKeyDown(KeyCode.Return) )
		{
		    CutSceneManager.IncreaseCutScene();
		}
                break;
            case 4: // walk away and reveal the hint.
		speech_bubble.close();
	        player.toggleThinkable(true);
		anim.SetBool("run", true);

                body.velocity = new Vector2(speed, body.velocity.y);
                body.GetComponent<SpriteRenderer>().flipX = true;
                MaskController.follow = true;
                break;
	        case 5:  //disappear and kill self.
		        MaskController.follow = false;
                AudioController.walkingNPC1=false;
                Destroy(gameObject);
		    break;
                default:
                Debug.Log("Fault in NPC1Movement");
                break;
        }

    }
}
