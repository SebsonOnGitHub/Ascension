using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class NPCJacobMovement : MonoBehaviour
{
    private Animator anim;
    public PlayerMovement player;
    public speech_bubble_controller speech_bubble;
    public string Dialogue;
    private bool talking,playerInColBox;
    public NPCThoughtBubbleController bubble;
    private KeyCode prevKey;
    

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("frontFacing", true);
	talking =false;
	playerInColBox = false;
	prevKey = KeyCode.None;
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
	if (other.name == "Player") {
	    playerInColBox = false;
	}
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
	if (other.name == "Player" )
	{
	    playerInColBox = true;
	}
    }

    void Update()
    {
	if(prevKey == KeyCode.None)
	{
	    if(Input.GetKey(KeyCode.Space) && !player.isThinking())
	    {
		prevKey = KeyCode.Space;
		if(talking)
		{
		    speech_bubble.close();
		    talking = false;
		    player.toggleThinkable(true);
		    player.toggleMovable(true);
		}
		else if(playerInColBox)
		{
		    float offset = transform.position.x-player.transform.position.x;
		    player.toggleThinkable(false);
		    player.toggleMovable(false);
		    speech_bubble.show(Dialogue,1,offset); //show(Dialogue);
		    talking = true;
		}
	    }
	}
	if (Input.GetKeyUp(prevKey))
	    prevKey= KeyCode.None;
    }
}
