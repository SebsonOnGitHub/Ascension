using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class ThinkingController : MonoBehaviour
{
    private PlayerMovement player;
    public TypingManager Typingmanager;
    public bool thinkingState;
    public bool showThought;

    private Animator anim;

    void Start()
    {
        thinkingState = false;
        anim = GetComponent<Animator>();
        player = GetComponentInParent<PlayerMovement>();
        anim.SetBool("thinking", false);
    }


    void Update()
    {
        transform.position = new Vector3(player.transform.position.x - 4.5f, player.transform.position.y + 3.7f, player.transform.position.z);

        if (Input.GetKeyDown(KeyCode.Return) & PlayerMovement.thinkable)
        {
            thinkingState = !thinkingState;
            anim.SetBool("thinking", thinkingState);
        }

    }

    public bool getThinkingState()
    {
        return thinkingState;
    }

    public void updateThought()
    {
        showThought = !showThought;
    }
    public void SetThought(string thought, string solution, UnityEvent solved ,UnityEvent notSolved)
    {
    	Typingmanager.SetThought(thought,solution,solved,notSolved);
    }
}
