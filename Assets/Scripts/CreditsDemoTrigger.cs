using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsDemoTrigger : MonoBehaviour
{
    public GameObject scroller;
    public PlayerMovement player;
    public AudioSource backgroundSource;
    public AudioClip demoCredits;
    private float volume;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        scroller.SetActive(true);
        player.toggleThinkable(false);
        player.toggleMovable(false);

        volume = backgroundSource.volume;
        StartCoroutine(Fade());
    }

    private IEnumerator Fade()
    {
        for (int x = 100; x > 0; x--)
        {
            backgroundSource.volume -= 0.0015f;
            yield return new WaitForSecondsRealtime(0.015f);
        }
        backgroundSource.clip = demoCredits;
        backgroundSource.volume = volume;
        backgroundSource.Play();
    }
}
