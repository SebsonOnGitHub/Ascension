using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class speech_bubble_controller : MonoBehaviour
{
    //public Text text;
    public TMP_Text text;
    private string CurrentText;
    const string kAlphaCode = "<color=#00000000>";
    const float kMaxTextTime = 0.5f;
    public int textSpeed=5;
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

	string originalText = CurrentText;
	string displayedText = "";
	int alphaIndex = 0;
	text.text = "";
	foreach(char c in CurrentText.ToCharArray())
	{
	    alphaIndex++;
	    text.text = originalText;
	    displayedText = text.text.Insert(alphaIndex,kAlphaCode);
	    text.text=displayedText;
	    yield return new WaitForSecondsRealtime(kMaxTextTime/textSpeed);
	}
	AudioController.Dialogue_sound = false;
    }
}
