using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
public class HolyLightController : MonoBehaviour
{
    public static bool move;
    public float speed;
    public float stop;
    private Light2D holyLight;
    void Start()
    {
        holyLight = GetComponent<Light2D>();
        //holyLight.pointLightOuterRadius =0.679f;
        //holyLight.pointLightInnerRadius =0.679f;
        move = true;
    }

    void FixedUpdate()
    {
        if (move & holyLight.pointLightOuterRadius <= stop)
	{
	    holyLight.pointLightOuterRadius += speed;
	    holyLight.pointLightInnerRadius += speed;
	}
	
    }
}
