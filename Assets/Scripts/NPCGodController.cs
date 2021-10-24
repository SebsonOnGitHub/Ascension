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
    public int talkingSpeed;
    public float voicePitch;
    private bool firstTalk;
    private bool playerInColBox;
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
	firstTalk = true;
    }

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.name == "Player")
		{
			playerInColBox = false;
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
    {
	dialogueParts[0] = Dialogue.Substring(0, cutOff1);
	dialogueParts[1] = Dialogue.Substring(cutOff1, cutOff2);
	dialogueParts[2] = Dialogue.Substring(cutOff1 + cutOff2, Dialogue.Length - (cutOff1 + cutOff2));
	dialoguePartIterator = 0;
	playerInColBox = true;
	if (other.name == "Player")
	{
	    player.toggleThinkable(false);
	    player.toggleMovable(false);
	    float offset = transform.position.x - player.transform.position.x;
	    speech_bubble.move(bubble_offset_x, bubble_offset_y);
	    offset = 0.0f; // temp test
	    speech_bubble.show(dialogueParts[dialoguePartIterator++],talkingSpeed, voicePitch);
	    talking = true;
	}
    }
    void Update() // fixedUpdate doesn't work here
    {
		player.toggleSpaceHint(playerInColBox & !talking, 3);
	
	if(prevKey == KeyCode.None)
	{
	    /********** DEBUG **********/
	    if(Input.GetKey(KeyCode.F1) && !player.isThinking() && firstTalk && talking) // player SECRET F1!! now we close dialogue box
	    {
		prevKey = KeyCode.F1;
		speech_bubble.close();
		if (dialoguePartIterator <= 2)
		{	
		    speech_bubble.show(dialogueParts[dialoguePartIterator],talkingSpeed, 0.21f);
		    dialoguePartIterator++;
		}
		else
		{
		    talking = false;
		    player.toggleThinkable(true);
		    player.SetThought("I fear to think I'm here", "I think therefore I am", goalReached, goalNotReached);
		    firstTalk = false;
			talking = false;
		    dialoguePartIterator = 0;
		}
	    }
	    
	    /********** ACTUAL CODE **********/
	    if(Input.GetKey(KeyCode.Space) && !player.isThinking())
	    {
		prevKey = KeyCode.Space;
		if (talking && (!firstTalk || speech_bubble.isDone())) // player entered space and now we close dialogue box
		{
		    speech_bubble.close();
		    if (dialoguePartIterator <= 2)
		    {
			speech_bubble.show(dialogueParts[dialoguePartIterator],talkingSpeed, voicePitch);
			dialoguePartIterator++;
		    }
		    else
		    {
			talking = false;
			player.toggleThinkable(true);
			if (firstTalk)
			{
			    player.SetThought("I fear to think I'm here", "I think therefore I am", goalReached, goalNotReached);
			    firstTalk = false;
			}
			
			dialoguePartIterator = 0;
		    }
		}
		else if(playerInColBox && !talking) // player pressed space and now we show dialogue box
		{
		    player.toggleThinkable(false);
		    player.toggleMovable(false);
		    float offset = transform.position.x-player.transform.position.x;
		    speech_bubble.move(bubble_offset_x, bubble_offset_y);
		    speech_bubble.show(dialogueParts[dialoguePartIterator++],talkingSpeed, voicePitch);
		    talking = true;
		}
	    }
	}
	if (Input.GetKeyUp(prevKey))
	    prevKey= KeyCode.None;
    }
}
