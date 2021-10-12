using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class HaloController : MonoBehaviour
{
    private Light2D halo;
    public float outerLightIncrease;
    public float innerLightIncrease;
    private void Start()
    {
        halo = GetComponent<Light2D>();
    }

    public void increaseBrightness()
    {
        //TODO: Increase the halo light
	halo.pointLightOuterRadius += outerLightIncrease;
        halo.pointLightInnerRadius += innerLightIncrease;
        Debug.Log("The light has been increased around the Halo!");
    }
}
