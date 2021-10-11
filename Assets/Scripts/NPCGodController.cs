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
    public float bubble_offset_x,bubble_offset_y;
    // Start is called before the first frame update
    void Start()
    {
	talking =false;
	prevKey = KeyCode.None;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
	if (other.name == "Player")
	{
	    player.toggleThinkable(false);
	    player.toggleMovable(false);
	    float offset = transform.position.x-player.transform.position.x;
	    speech_bubble.transform.position = new Vector3(speech_bubble.transform.position.x,bubble_offset_y,speech_bubble.transform.position.z);
	    offset = 0.0f; // temp test
	    speech_bubble.show(Dialogue,2,bubble_offset_x, 0.21f);
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
		if(talking) // player entered space and now we close dialogue box
		{
		    
		    player.SetThought("MONKS","MONK",goalReached,goalNotReached);
		    //Destroy(GetComponent<BoxCollider2D>());

		    speech_bubble.close();
		    talking = false;
		    player.toggleThinkable(true);
		}
	    }
	}
	if (Input.GetKeyUp(prevKey))
	    prevKey= KeyCode.None;
    }
}
