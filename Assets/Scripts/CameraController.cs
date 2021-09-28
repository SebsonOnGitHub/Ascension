using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;

    private void FixedUpdate()
    {
        transform.position = new Vector3(player.position.x, player.position.y + 3, transform.position.z);
    }
}
