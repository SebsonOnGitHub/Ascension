using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog_controller : MonoBehaviour
{
    // Start is called before the first frame update
    private  Animator anim;
    private BoxCollider2D boxCollider;
    public PlayerMovement player;
    private bool nice;
    void Start()
    {
	nice = false;
	anim = GetComponent<Animator>();
	boxCollider = GetComponent<BoxCollider2D>();
    }
    public void bark()
    {
	anim.SetTrigger("Bark");
    }
    
    public void stop_bark()
    {
	anim.SetTrigger("Stop_bark");
	AudioController.Dog_bark=false;
	
        PlayerMovement.CutSceneRun(-200.0f,5.1f);
	player.toggleThinkable(true);
	player.toggleMovable(true);
	
    }
    /*
    public void Stop_Transition()
    {
	//remove block of doggy
	anim.SetTrigger("Nice");
        
    }
    */
    private void OnTriggerEnter2D(Collider2D other)
    {
	if (nice)
	    return;
	
        player.toggleThinkable(false);
        player.toggleMovable(false);
	AudioController.Dog_bark=true;
	anim.SetTrigger("Bark");
	
    }
    public void Become_nice()
    {
        Destroy(boxCollider);
	anim.SetTrigger("Transition");

    }
    
    // Update is called once per frame
    void Update()
    {
	switch (CutSceneManager.getCurrentCutScene())
	{
	    case 5:
	
		//Put animation for divine dog transformation here
				Debug.Log("Cutscene 5");
		break;

	    case 6:
		//dog transforms here
        

		break;
		
	    
	}

	
    }
}
