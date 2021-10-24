using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ThinkingController : MonoBehaviour
{

    public TypingManager Typingmanager;
    public bool thinkingState;
    public bool currThinking;
    private PlayerMovement player;
    private Animator anim;
    public float offsetX = -2f;
    public float offsetY = 2.4f;

    void Start()
    {
        thinkingState = false;
        currThinking = false;
        anim = GetComponent<Animator>();
        player = GetComponentInParent<PlayerMovement>();
        anim.SetBool("thinking", false);
    }


    void Update() // fixedUpdate doesn't work here
    {
        transform.position = new Vector3(player.transform.position.x + offsetX, player.transform.position.y + offsetY, player.transform.position.z);

        if (Input.GetKeyDown(KeyCode.Return) & player.thinkable & !currThinking)
	{
	    anim.SetBool("thinking", !anim.GetBool("thinking"));
	}
    }

    public bool getThinkingState()
    {
        return thinkingState || currThinking;
    }
    public bool isThinkingIdle()
    {
	    return thinkingState && !currThinking;
    }
    public void setThinkingState()
    {
        thinkingState = true;
    }
    public void unsetThinkingState()
    {
	thinkingState = false;
    }

    public void setCurrThinking()
    {
        currThinking = !currThinking;
    }
    public void addSolution(string solution)
    {
	Typingmanager.addSolution(solution);
    }
    public void SetThought(string thought, string solution, UnityEvent solved ,UnityEvent notSolved,string hint = "")
    {
	List<string> sol = new List<string>(){solution};
    	Typingmanager.SetThought(thought,sol,solved,notSolved,hint);
        if (thought != "")
            StartCoroutine(player.newThought());
    }
}
