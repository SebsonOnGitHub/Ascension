using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC1Movement : MonoBehaviour
{
    public float speed;
    public GameObject speech_bubble;
    public PlayerMovement player;

    private Rigidbody2D body;
    private Animator anim;
    private GameObject bubble_object;

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
            case 1: //walk towards player
                anim.SetBool("run", true);
                body.velocity = new Vector2(-speed, body.velocity.y);
                break;
            case 2: // stop walking, spawn speech bubble
                anim.SetBool("run", false);
                body.velocity = new Vector2(0, body.velocity.y);
		        bubble_object = Instantiate(speech_bubble, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
	            bubble_object.transform.parent = GameObject.Find("Monk1").transform;
                CutSceneManager.IncreaseCutScene();
		        break;
            case 3: // start talking until player inputs "return"
                if (Input.GetKeyDown(KeyCode.Return))
                    CutSceneManager.IncreaseCutScene();
                break;
            case 4: // walk away and reveal the hint.
	            Destroy(bubble_object);
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
