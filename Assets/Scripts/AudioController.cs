using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource[] audioSources;
    public float minWaitBetweenPlays;
    public float maxWaitBetweenPlays;
    public static bool walkingPlayer;
    public static bool walkingNPC1;

    private float waitTimeCountdownBirds;


    private void Start()
    {
        audioSources = GetComponentsInChildren<AudioSource>();
        waitTimeCountdownBirds = Random.Range(minWaitBetweenPlays, maxWaitBetweenPlays);
        walkingPlayer = false;
        walkingNPC1 = false;
    }


    void Update()
    {
        foreach (AudioSource source in audioSources)
        {
            if (source.name.Equals("WalkingPlayer"))
            {
                if (walkingPlayer & !source.isPlaying)
                    source.Play();

                if (!walkingPlayer & source.isPlaying)
                    source.Stop();
            }

            if (source.name.Equals("WalkingNPC1"))
            {
                if (walkingNPC1 & !source.isPlaying)
                    source.Play();

                if (!walkingNPC1 & source.isPlaying)
                    source.Stop();
            }

            if (source.name.Equals("BirdAudio") & !source.isPlaying)
            {
                if (waitTimeCountdownBirds < 0f)
                {
                    source.Play();
                    waitTimeCountdownBirds = Random.Range(minWaitBetweenPlays, maxWaitBetweenPlays);
                }
                else
                    waitTimeCountdownBirds -= Time.deltaTime;
            }
        }
        
    }

}
