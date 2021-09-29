using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS2Trigger : MonoBehaviour
{
    private bool once = true;
    public int triggerNumber;
    public speech_bubble_controller speech_bubble;
    private void OnTriggerEnter2D(Collider2D other)
    {
	if(once)
	{
	    PlayerMovement.toggleThinkable(false);
	    speech_bubble.show("hej kom och köp mina päron snälla");
	    once=false;
	}
        if (triggerNumber == CutSceneManager.getCurrentCutScene()+1)
        {
            CutSceneManager.IncreaseCutScene();
            Destroy(gameObject);
        }
    }
}
