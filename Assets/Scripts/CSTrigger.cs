using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSTrigger : MonoBehaviour
{
    public int triggerNumber;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (triggerNumber == CutSceneManager.getCurrentCutScene()+1)
        {
            CutSceneManager.IncreaseCutScene();
            Destroy(gameObject);
        }
    }
}
