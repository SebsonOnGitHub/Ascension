using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneManager : MonoBehaviour
{
    public static int currentCutscene;

    private void Start()
    {
        currentCutscene = 0;
    }

    // Update is called once per frame
    public static void IncreaseCutScene()
    {
        currentCutscene++;
    }

    public static int getCurrentCutScene()
    {
        return currentCutscene;
    }
}
