using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog_controller : MonoBehaviour
{
    // Start is called before the first frame update
    private  Animator anim;
    private BoxCollider2D boxCollider;
    
    void Start()
    {
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
	PlayerMovement.toggleThinkable(true);
	PlayerMovement.toggleMovable(true);
	
    }
    public void Stop_Transition()
    {
	anim.SetTrigger("Nice");
	PlayerMovement.toggleThinkable(true);
	PlayerMovement.toggleMovable(true);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
	PlayerMovement.toggleThinkable(false);
	PlayerMovement.toggleMovable(false);
	AudioController.Dog_bark=true;
	anim.SetTrigger("Bark");
	
    }
    public void Become_nice()
    {
	PlayerMovement.toggleThinkable(false);
	PlayerMovement.toggleMovable(false);
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
