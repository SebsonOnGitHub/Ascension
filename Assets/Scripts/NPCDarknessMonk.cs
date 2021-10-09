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
	if (other.name == "Halo" )
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
				player.SetThought("a holy being is human", "being a human is holy", goalReached, goalNotReached);
				ThoughtSizeController.setFontSize(30);
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
			sprite.flipX=false;
		    }else
		    {
			sprite.flipX=true;
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
 
