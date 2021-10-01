using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class speech_bubble_controller : MonoBehaviour
{
    public Text text;
    private string CurrentText;
    
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void show(string textarg)
    {
        gameObject.SetActive(true);
	    CurrentText = textarg;
	    StartCoroutine(DisplayText());
    }
    
    public void close()
    {
	    gameObject.SetActive(false);
	    StopAllCoroutines();
    }

    private IEnumerator DisplayText()
    {
        text.text = "";
	    foreach (char c in CurrentText.ToCharArray())
	    {
            if(c != ' ')
                AudioController.Dialogue_sound = true;
            text.text +=c;
	        yield return new WaitForSecondsRealtime(0.1f);
	    }
	    AudioController.Dialogue_sound = false;
    }
}
