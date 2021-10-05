using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
public class HolyLightController : MonoBehaviour
{
    public static bool move;
    public float speed;
    public float stop;
    public Light2D light;
    void Start()
    {
	light.pointLightOuterRadius =0.679f;
	light.pointLightInnerRadius =0.679f;
        move = true;
    }

    void FixedUpdate()
    {
        if (move & light.pointLightOuterRadius <= stop)
	{
	    light.pointLightOuterRadius += speed;
	    light.pointLightInnerRadius +=0.786f*speed;
	}
	

	//transform.position = new Vector3(transform.position.x, transform.position.y - speed, transform.position.z);
    }
}
