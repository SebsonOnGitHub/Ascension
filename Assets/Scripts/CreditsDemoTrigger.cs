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

        volume = backgroundSource.volume;
        StartCoroutine(Fade());
    }

    private IEnumerator Fade()
    {
        for (int x = 25; x > 0; x--)
        {
            backgroundSource.volume -= 0.0012f;
            yield return new WaitForSecondsRealtime(0.06f);
        }

        backgroundSource.clip = demoCredits;
        backgroundSource.Play();

        for (int x = 25; x > 0; x--)
        {
            backgroundSource.volume += 0.006f;
            if (backgroundSource.volume >= volume)
                break;
            yield return new WaitForSecondsRealtime(0.06f);
        }

        backgroundSource.volume = volume;
    }
}
