using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameTest : MonoBehaviour
{
    public Rigidbody2D body;
    public float flySpeedPublic;
    private static float flySpeed;
    private static bool fly;

    private void Start()
    {
        fly = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        fly = true;
        flySpeed = flySpeedPublic;
    }

    private void Update()
    {
        if (fly)
            body.velocity = new Vector2(0, flySpeed);
    }

}
