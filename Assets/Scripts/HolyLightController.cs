using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyLightController : MonoBehaviour
{
    public static bool move;
    public float speed;
    public float stop;

    void Start()
    {
        move = true;
    }

    void Update()
    {
        if (move & transform.position.y > stop)
            transform.position = new Vector3(transform.position.x, transform.position.y - speed, transform.position.z);
    }
}
