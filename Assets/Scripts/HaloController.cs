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
        StartCoroutine(Fade());
	    //halo.pointLightOuterRadius += outerLightIncrease;
        //halo.pointLightInnerRadius += innerLightIncrease;
    }

    private IEnumerator Fade()
    {
        float newOuterMax = halo.pointLightOuterRadius + outerLightIncrease;
        float newInnerMax = halo.pointLightInnerRadius + innerLightIncrease;

        while (halo.pointLightOuterRadius < newOuterMax && halo.pointLightInnerRadius < newInnerMax)
        {
            halo.pointLightOuterRadius += 0.01f;
            halo.pointLightInnerRadius += 0.01f;
            yield return new WaitForSecondsRealtime(0.003f);
        } 
    }
}
