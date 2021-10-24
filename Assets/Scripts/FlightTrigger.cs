using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightTrigger : MonoBehaviour
{
    public AudioSource backgroundSource;
    public AudioClip flight;
    private float volume;

    public void flightStart()
    {
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

        backgroundSource.clip = flight;
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
