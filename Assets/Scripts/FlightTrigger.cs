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
        for (int x = 100; x > 0; x--)
        {
            backgroundSource.volume -= 0.0003f;
            yield return new WaitForSecondsRealtime(0.015f);
        }

        backgroundSource.clip = flight;
        backgroundSource.Play();

        for (int x = 100; x > 0; x--)
        {
            backgroundSource.volume += 0.0015f;
            if (backgroundSource.volume >= volume)
                break;
            yield return new WaitForSecondsRealtime(0.015f);
        }

        backgroundSource.volume = volume;
    }
}
