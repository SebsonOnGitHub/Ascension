using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speech_Bubble_Controller : MonoBehaviour
{

    public string text;
    
    //private Animator anim;
    private NPC1Movement npc; // should be generic for all NPCs, probably need some sort of generic class for all npc:s containing at least their current position.
    public float scaleX,scaleY;
    void Start()
    {
	
	npc = GetComponentInParent<NPC1Movement>();
	transform.position = new Vector3(npc.transform.position.x-0.15f , npc.transform.position.y + 1.37f, npc.transform.position.z);
        transform.localScale = new Vector3(-0.55f,0.34f,transform.localScale.z);
    }
    
    void FixedUpdate()
    {
	
	transform.position = new Vector3(npc.transform.position.x-0.15f , npc.transform.position.y + 1.37f, npc.transform.position.z);
        transform.localScale = new Vector3(-0.55f,0.34f,transform.localScale.z);
    }
    public void setScale(float argX,float argY)
    {
	scaleX=argX;
	scaleY=argY;
    }
    
}
