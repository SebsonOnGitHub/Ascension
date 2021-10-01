using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue_box_controller : MonoBehaviour
{
    public float offset_x,offset_y;
    public Transform monk;
    private SpriteRenderer this_dialogue_box;
    public speech_bubble_controller speech_bubble;
    // Start is called before the first frame update
    void Start()
    {
	this_dialogue_box = GetComponent<SpriteRenderer>();
	this_dialogue_box.enabled = false;
        transform.position = new Vector3(monk.position.x+offset_x,monk.position.y+offset_y,transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(monk.position.x+offset_x,monk.position.y+offset_y,transform.position.z);
    }
    public void show(string textarg)
    {
	this_dialogue_box.enabled = true; // enable the renderer
	speech_bubble.show(textarg);
    }
    public void close()
    {
	speech_bubble.close();
	this_dialogue_box.enabled = false; // enable the renderer
    }
}
