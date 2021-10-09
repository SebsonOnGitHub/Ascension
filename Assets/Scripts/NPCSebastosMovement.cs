using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class NPCSebastosMovement : MonoBehaviour
{
    public PlayerMovement player;
    public speech_bubble_controller speech_bubble;
    public string Dialogue;
    private bool talking, UnfreezeNext,firstTalk,playerInColBox;
    public UnityEvent goalReached;
    public UnityEvent goalNotReached;
    private KeyCode prevKey;
    private SpriteRenderer sprite;
    void Start()
    {
	sprite = GetComponent<SpriteRenderer>();
        talking =false;
	UnfreezeNext = false;
	firstTalk = true;
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
	    if(firstTalk)
	    {
		player.toggleThinkable(false);
		player.toggleMovable(false);
		float offset = transform.position.x-player.transform.position.x;
		speech_bubble.show (Dialogue,-1,offset);
		talking = true;
	    }
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
		    if (firstTalk)
		    {
			player.SetThought("What's the deal with mute monks?", "What's the deal with mute monks", goalReached,goalNotReached);
			firstTalk = false;
			Destroy(GetComponent<BoxCollider2D>());
		    }
		    speech_bubble.close();
		    talking = false;
		    player.toggleThinkable(true);
		    player.toggleMovable(true);
		}
		else if(playerInColBox)
		{
		    if(player.transform.position.x > transform.position.x)
		    {
			sprite.flipX=true;
		    }else
		    {
			sprite.flipX=false;
		    }
		    float offset = transform.position.x-player.transform.position.x;
		    player.toggleThinkable(false);
		    player.toggleMovable(false);
		    speech_bubble.show(Dialogue,-1,offset);
		    talking = true;
		}
	    }
	}
	if (Input.GetKeyUp(prevKey))
	    prevKey= KeyCode.None;
    }
 
}
