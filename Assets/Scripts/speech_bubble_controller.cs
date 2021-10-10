using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class speech_bubble_controller : MonoBehaviour
{
    public float posX;
    private float startPosX;
    public TMP_Text text;
    private string CurrentText;
    const string kAlphaCode = "<color=#00000000>";
    const float kMaxTextTime = 0.5f;
    public int textSpeed = 5;
    private RectTransform rt;
    const float canvasToWorldFactor = 65.0f;
    
    void Start()
    {
	rt = GetComponent<RectTransform>();
	startPosX = posX;
        gameObject.SetActive(false);
    }
    public void show(string textarg , int speed=-1 ,float offset_x = 0)
    {	
	//transform.position = new Vector3(transform.position.x,transform.position.y,transform.position.z);
		rt.anchoredPosition = Vector3.right*offset_x*canvasToWorldFactor;
	//        rt.anchoredPosition.localPosition += Vector3.Right;
	if (speed == -1)
	    speed = textSpeed;
        gameObject.SetActive(true);
	CurrentText = textarg;
	StartCoroutine(DisplayText(speed));
    }
    
    public void close()
    {
	gameObject.SetActive(false);
	StopCoroutine(DisplayText(0));
    }

    private IEnumerator DisplayText(int speed)
    {
	string originalText = CurrentText;
	string displayedText = "";
	int alphaIndex = 0;
	text.text = "";
	foreach(char c in CurrentText.ToCharArray())
	{
	    alphaIndex++;
	    text.text = originalText;
	    if(c != ' ')
                AudioController.Dialogue_sound = true;
	    displayedText = text.text.Insert(alphaIndex,kAlphaCode);
	    text.text=displayedText;
	    yield return new WaitForSecondsRealtime(kMaxTextTime/speed);
	}
	AudioController.Dialogue_sound = false;
    }
    
}
