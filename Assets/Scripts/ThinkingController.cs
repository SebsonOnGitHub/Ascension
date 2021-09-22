using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThinkingController : MonoBehaviour
{
    private PlayerMovement player;

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

        if (Input.GetKeyDown(KeyCode.Return))
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
}
