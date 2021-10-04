using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSongToggler : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        AudioController.birdSong = collision.transform.position.x < transform.position.x;
    }
}
