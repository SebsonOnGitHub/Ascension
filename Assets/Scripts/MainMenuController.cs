using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class MainMenuController : MonoBehaviour
{
    private bool firstEnable = true;
    public MainMenuController backToScreen;

    void Start()
    {
        SelectFirstElement();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) & backToScreen != null)
        {
            backToScreen.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }

    private void OnEnable()
    {
        if (!firstEnable | this.gameObject.name != "MainMenu")
        {
                SelectFirstElement();
        }
        else
            firstEnable = false;
    }

    private void SelectFirstElement()
    {
        foreach (Selectable selectable in transform.GetComponentsInChildren<Selectable>())
        {
            if(selectable.tag == "FirstElement")
            {
                selectable.Select();
            }
        }
    }
}
