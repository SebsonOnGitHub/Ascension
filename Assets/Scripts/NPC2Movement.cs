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

    void Start()
    {
        talking =false;
		UnfreezeNext = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        player.toggleThinkable(false);
		player.toggleMovable(false);
		speech_bubble.show(Dialogue);
        talking = true;
        Destroy(GetComponent<BoxCollider2D>());
    }

    void Update()
    {
		if(!talking & UnfreezeNext)
		{
			player.toggleThinkable(true);
			player.toggleMovable(true);
			UnfreezeNext = false;
		}
	
		if(talking & Input.GetKeyDown(KeyCode.Return))
		{
			speech_bubble.close();
			talking = false;
			UnfreezeNext = true;

            player.SetThought("there is a dog","there is a god",goalReached,goalNotReached);
        }
    }
}
