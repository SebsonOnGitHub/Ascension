using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class NPCGodController : MonoBehaviour
{
	public PlayerMovement player;
	public speech_bubble_controller speech_bubble;
	public UnityEvent goalReached;
	public UnityEvent goalNotReached;
	private bool talking;
	public string Dialogue;
	private KeyCode prevKey;
	public float bubble_offset_x, bubble_offset_y;
	private string[] dialogueParts;
	public int cutOff1,cutOff2;
	private int dialoguePartIterator;
	public int speakingSpeed;
	// Start is called before the first frame update
	void Start()
	{
		dialogueParts = new string[3];
		dialogueParts[0] = Dialogue.Substring(0, cutOff1);
		dialogueParts[1] = Dialogue.Substring(cutOff1, cutOff2);
		dialogueParts[2] = Dialogue.Substring(cutOff1+cutOff2, Dialogue.Length - 1 - (cutOff1+cutOff2));
		dialoguePartIterator = 0;
		talking =false;
		prevKey = KeyCode.None;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
		dialogueParts[0] = Dialogue.Substring(0, cutOff1);
		dialogueParts[1] = Dialogue.Substring(cutOff1, cutOff2);
		dialogueParts[2] = Dialogue.Substring(cutOff1 + cutOff2, Dialogue.Length - (cutOff1 + cutOff2));
		dialoguePartIterator = 0;

		if (other.name == "Player")
		{
			player.toggleThinkable(false);
			player.toggleMovable(false);
			float offset = transform.position.x - player.transform.position.x;
			speech_bubble.move(bubble_offset_x, bubble_offset_y);
			offset = 0.0f; // temp test
			speech_bubble.show(dialogueParts[dialoguePartIterator++],speakingSpeed, 0.21f);
			talking = true;
		}
	/*
	  else if(playerInColBox) // player pressed space and now we show dialogue box
	  {
	  if(player.transform.position.x > transform.position.x)
	  {
	  sprite.flipX=true;
	  }else
	  {
	  sprite.flipX=false;
	  }
	  player.toggleThinkable(false);
	  player.toggleMovable(false);
	  float offset = transform.position.x-player.transform.position.x;
	  speech_bubble.show(Dialogue,-1,offset);
	  talking = true;
	  }
	*/
    }
    void Update()
    {
	if(prevKey == KeyCode.None)
	{
	    if(Input.GetKey(KeyCode.Space) && !player.isThinking())
	    {
			prevKey = KeyCode.Space;
				if (talking) // player entered space and now we close dialogue box
				{
					
					//Destroy(GetComponent<BoxCollider2D>());

					speech_bubble.close();
					if (dialoguePartIterator <= 2)
					{	
						speech_bubble.show(dialogueParts[dialoguePartIterator],speakingSpeed, 0.21f);
						dialoguePartIterator++;
					}
				else
				{
					talking = false;
					player.toggleThinkable(true);
						
					player.SetThought("I fear to think I'm here", "I think therefore I am", goalReached, goalNotReached);
					dialoguePartIterator = 0;
				}
			}
	    }
	}
	if (Input.GetKeyUp(prevKey))
	    prevKey= KeyCode.None;
    }
}
