using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCJacobThoughtController : MonoBehaviour
{
    private SpriteRenderer sprite;
    public NPCThoughtBubbleController bubbleText;
    // Start is called before the first frame update
    void Start()
    {
	sprite = GetComponent<SpriteRenderer>();
	sprite.enabled = false;
    }

    public void Think()
    {
	//Jacobs thought bubble animation should be called before we're here.
	sprite.enabled = true;
	bubbleText.show("Zlatan är Sveriges Konung över Sverige.");
	
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
