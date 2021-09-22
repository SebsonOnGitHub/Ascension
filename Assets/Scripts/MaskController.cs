using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskController : MonoBehaviour
{
    public static bool follow;
    public NPC1Movement NPC;

    void Start()
    {
        follow = false;
    }

    void Update()
    {
        if(follow)
            transform.position = new Vector3(NPC.transform.position.x - 9f, transform.position.y, transform.position.z);
    }
}
