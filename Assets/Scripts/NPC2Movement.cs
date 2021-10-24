using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class NPC2Movement : MonoBehaviour
{
    public PlayerMovement player;
    public speech_bubble_controller speech_bubble;
    public string Dialogue;
    private bool talking, firstTalk,playerInColBox;
    public UnityEvent goalReached;
    public UnityEvent goalNotReached;
    private KeyCode prevKey;
    private SpriteRenderer sprite;
    public float voicePitch;
    public int talkingSpeed;
    public string puzzleHint;
    void Start()
    {
	sprite = GetComponent<SpriteRenderer>();
        talking =false;
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
		if (other.name == "Player" && !player.isThinking() )
		{
			playerInColBox = true;
			if(firstTalk)
			{
			player.toggleThinkable(false);
			player.toggleMovable(false);
			float offset = transform.position.x-player.transform.position.x;
			speech_bubble.move(offset,0.0f);
			speech_bubble.show(Dialogue,talkingSpeed,voicePitch);
			talking = true;
			}
		}
    }

    void Update() // fixedUpdate doesn't work here
    {
		player.toggleSpaceHint(playerInColBox & !talking, 0);

		if (prevKey == KeyCode.None)
	{
	    /********** DEBUG **********/
	    if(talking & firstTalk & Input.GetKey(KeyCode.F1) && !player.isThinking()  )
	    {
		speech_bubble.close();
		player.SetThought("there is a dog","there is a god",goalReached,goalNotReached,puzzleHint);
		firstTalk = false;
		talking = false;
		player.toggleThinkable(true);
		player.toggleMovable(true);
		Destroy(GetComponent<BoxCollider2D>());
	    }
	    
	    /*********** ACTUAL CODE **********/
	    if(Input.GetKey(KeyCode.Space) && !player.isThinking()  )
	    {
		prevKey = KeyCode.Space;
		if(talking) // player entered space and now we close dialogue box
		{
		    
		    if (firstTalk && speech_bubble.isDone() ) 
		    {
			player.SetThought("there is a dog","there is a god",goalReached,goalNotReached,puzzleHint);
			firstTalk = false;
			Destroy(GetComponent<BoxCollider2D>());
		    }
		    if (!firstTalk)
		    {
			speech_bubble.close();
			talking = false;
			player.toggleThinkable(true);
			player.toggleMovable(true);
		    }
		}
		else if(playerInColBox) // player pressed space and now we show dialogue box
		{
		    if(player.transform.position.x > transform.position.x)
		    {
			sprite.flipX=true;
		    }else
		    {
			sprite.flipX=false;
		    }
		    
		    player.flipX(!sprite.flipX);
		    player.toggleThinkable(false);
		    player.toggleMovable(false);
		    float offset = transform.position.x-player.transform.position.x;
		    speech_bubble.move(offset,0.0f);
		    speech_bubble.show(Dialogue, talkingSpeed, voicePitch);
		    talking = true;
		}
	    }
	}
	if (Input.GetKeyUp(prevKey))
	    prevKey= KeyCode.None;
    }
}
