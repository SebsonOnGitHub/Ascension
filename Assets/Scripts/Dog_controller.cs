using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog_controller : MonoBehaviour
{
    private  Animator anim;
    private BoxCollider2D boxCollider;
    public PlayerMovement player;

    void Start()
    {
	    anim = GetComponent<Animator>();
	    boxCollider = GetComponent<BoxCollider2D>();
    }
    
    public void stop_bark()
    {
	    PlayerMovement.CutSceneRun(-600.0f,5.1f);
	    player.toggleThinkable(true);
	    player.toggleMovable(true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        anim.SetTrigger("Bark");
        player.toggleThinkable(false);
        player.toggleMovable(false);
	    AudioController.Dog_bark=true;
    }
    public void Become_nice()
    {
        Destroy(boxCollider);
	anim.SetTrigger("Transition");
	AudioController.Woosh=true;

    }
}
