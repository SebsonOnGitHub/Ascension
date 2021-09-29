using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasFader : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private MainMenuController mainController;
    private bool fade;
    
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        mainController = GetComponentInChildren<MainMenuController>();
        fade = false;
    }

    private void FixedUpdate()
    {
        if(fade)
        {
            canvasGroup.alpha -= 0.02f;

            if (canvasGroup.alpha <= 0)
                mainController.PlayGame();
        }
    }

    public void fadeToGame()
    {
        fade = true;
    }
}
