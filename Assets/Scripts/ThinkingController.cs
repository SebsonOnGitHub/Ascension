using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThinkingController : MonoBehaviour
{
    public bool thinkingState;
    public bool currThinking;
    private PlayerMovement player;
    private Animator anim;

    void Start()
    {
        thinkingState = false;
        currThinking = false;
        anim = GetComponent<Animator>();
        player = GetComponentInParent<PlayerMovement>();
        anim.SetBool("thinking", false);
    }


    void Update()
    {
        transform.position = new Vector3(player.transform.position.x - 4.5f, player.transform.position.y + 3.9f, player.transform.position.z);

        if (Input.GetKeyDown(KeyCode.Return) & player.thinkable & !currThinking)
        {
            anim.SetBool("thinking", !anim.GetBool("thinking"));
        }

    }

    public bool getThinkingState()
    {
        return thinkingState;
    }

    public void setThinkingState()
    {
        thinkingState = !thinkingState;
    }

    public void setCurrThinking()
    {
        currThinking = !currThinking;
    }
}
