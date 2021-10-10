using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class HaloController : MonoBehaviour
{
    private Light2D halo;

    private void Start()
    {
        halo = GetComponent<Light2D>();
    }

    public void increaseBrightness()
    {
        //TODO: Increase the halo light
        Debug.Log("The light has been increased around the Halo!");
    }
}
