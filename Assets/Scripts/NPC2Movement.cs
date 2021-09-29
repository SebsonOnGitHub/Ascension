using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class NPC2Movement : MonoBehaviour
{
    public PlayerMovement player;
    public speech_bubble_controller speech_bubble; 
    public string Dialogue;
    private bool talking, UnfreezeNext;
    public UnityEvent goalReached;
    public UnityEvent goalNotReached;
    // Start is called before the first frame update
    void Start()
    {
	
        talking =false;
	UnfreezeNext = false;
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other)
    {
	PlayerMovement.toggleThinkable(false);
	PlayerMovement.toggleMovable(false);
	speech_bubble.show(Dialogue);
        talking = true;
    }
    void Update()
    {
	if(!talking & UnfreezeNext)
	{
	    PlayerMovement.toggleThinkable(true);
	    PlayerMovement.toggleMovable(true);
	    UnfreezeNext = false;
	}
	
	if(talking & Input.GetKeyDown(KeyCode.Return))
	{
	    speech_bubble.close();
	    talking = false;
	    UnfreezeNext = true;
	    

	    //aquire new thought here!!
	    player.SetThought("there is a dog","there is a god",goalReached,goalNotReached);
	    
	}
    }
}
