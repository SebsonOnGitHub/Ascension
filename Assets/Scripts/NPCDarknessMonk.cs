using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class NPCDarknessMonk : MonoBehaviour
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
		if (other.name == "Halo") {
			playerInColBox = false;
		}
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
	if (other.name == "Halo" && !player.isThinking() )
	{
	    playerInColBox = true;
	    if(firstTalk)
	    {
		float offset = transform.position.x-player.transform.position.x;
		speech_bubble.move(offset,0.0f);
		speech_bubble.show (Dialogue,talkingSpeed,voicePitch);
		talking = true;
		if(player.transform.position.x > transform.position.x)
		{
		    sprite.flipX=false;
		}else
		{
		    sprite.flipX=true;
		}
		player.flipX(sprite.flipX);
		player.toggleThinkable(false);
		player.toggleMovable(false);
	    }
	}
    }

    
    void Update()
    {
	if(prevKey == KeyCode.None)
	{
        /********** DEBUG **********/
	    if(talking && firstTalk && Input.GetKey(KeyCode.F1) && !player.isThinking()  )
	    {
		//prevKey = KeyCode.F1;
		speech_bubble.close();
		ThoughtSizeController.setFontSize(30);
		player.SetThought("a holy being is human", "being a human is holy", goalReached, goalNotReached);
		player.addSolution("being human is holy");
		firstTalk = false;
		player.toggleThinkable(true);
		player.toggleMovable(true);
		//Destroy(GetComponent<BoxCollider2D>());
	    }
	    
	    /*********** ACTUAL CODE **********/
	    if(Input.GetKey(KeyCode.Space) && !player.isThinking() )
	    {
		prevKey = KeyCode.Space;
		if(talking) // player entered space and now we close dialogue box
		{
		    if (speech_bubble.isDone() && firstTalk )
		    {
			Debug.Log(speech_bubble.isDone());
			firstTalk = false;
			ThoughtSizeController.setFontSize(30);
			player.SetThought("a holy being is human", "being a human is holy", goalReached, goalNotReached);
			player.addSolution("being human is holy");
			//Destroy(GetComponent<BoxCollider2D>());
		    }
		    if(!firstTalk)
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
			sprite.flipX=false;
		    }else
		    {
			sprite.flipX=true;
		    }
		    
		    player.flipX(sprite.flipX);
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
 
