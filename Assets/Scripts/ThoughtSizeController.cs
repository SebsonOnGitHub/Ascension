using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThoughtSizeController : MonoBehaviour
{
    public static Text[] displays;

    void Start()
    {
        displays = GetComponentsInChildren<Text>();
    }

    public static void setFontSize(int size)
    {
        displays[0].fontSize = size;
        displays[1].fontSize = size;
        displays[2].fontSize = size;
    }
}
