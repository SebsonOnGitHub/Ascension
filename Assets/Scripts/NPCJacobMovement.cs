using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCJacobMovement : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("frontFacing", true);
    }

}
