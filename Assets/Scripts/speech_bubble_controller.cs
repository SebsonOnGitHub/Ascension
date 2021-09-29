using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class speech_bubble_controller : MonoBehaviour
{
    public Text text;
    private string CurrentText;
    
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void show(string textarg)
    {
        gameObject.SetActive(true);
	CurrentText = textarg;
	StartCoroutine(DisplayText());
    }
    
    public void close()
    {
	AudioController.Dialogue_sound = false;
	gameObject.SetActive(false);
	StopAllCoroutines();
    }
    private IEnumerator DisplayText()
    {
	AudioController.Dialogue_sound = true;

	text.text = "";
	foreach(char c in CurrentText.ToCharArray())
	{
	    text.text +=c;
	    yield return new WaitForSecondsRealtime(0.1f);
	}
	AudioController.Dialogue_sound = false;
    }
}
