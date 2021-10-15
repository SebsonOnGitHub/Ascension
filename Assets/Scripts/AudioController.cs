using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    public AudioSource[] audioSources;
    public float minWaitBetweenPlays;
    public float maxWaitBetweenPlays;
    public static bool birdSong;
    public static bool walkingPlayer;
    public static bool walkingNPC1;
    public static bool Dialogue_sound;
    public static bool Dog_bark;
    public static bool Transform;
    private float waitTimeCountdownBirds;


    private void Start()
    {
        audioSources = GetComponentsInChildren<AudioSource>();
        waitTimeCountdownBirds = Random.Range(minWaitBetweenPlays, maxWaitBetweenPlays);
        walkingPlayer = false;
        walkingNPC1 = false;
	    Dialogue_sound = false;
	    Dog_bark = false;
        Transform = false;
        birdSong = true;
    }
    
    
    void FixedUpdate()
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
	    
            if (birdSong & source.name.Equals("BirdAudio") & !source.isPlaying)
            {
                if (waitTimeCountdownBirds < 0f)
                {
                    source.Play();
                    waitTimeCountdownBirds = Random.Range(minWaitBetweenPlays, maxWaitBetweenPlays);
                }
                else
                    waitTimeCountdownBirds -= Time.deltaTime;
            }

	    if(source.name.Equals("Dialogue_sound"))
	    {
                if (Dialogue_sound & source.isPlaying)
                    source.Stop();
		
                if (Dialogue_sound &!source.isPlaying)
                {
                    source.Play();
                    Dialogue_sound = false;
		}
	    }
	    
	    if(source.name.Equals("dog_bark"))
	    {
		if (Dog_bark &!source.isPlaying)
		{
		    source.Play();
		    Dog_bark = false;
		}
		
	    }
	    if(source.name.Equals("Transform"))
	    {
		if (Transform &!source.isPlaying)
		{
		    source.Play();
            Transform = false;
		}
		
	    }
	}
    }
}
