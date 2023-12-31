﻿using System.Collections;
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
    public AudioSource dialogue;
    private bool donePrinting;
    public TMP_Text hint;
    
    void Start()
    {
	rt = GetComponent<RectTransform>();
	startPosX = posX;
        gameObject.SetActive(false);
	donePrinting = true;
	hint.gameObject.SetActive(false);
    }
    public bool isDone()
    {
	return donePrinting;
    }
    public void move (float offset_x,float offset_y)
    {
	rt.anchoredPosition = Vector3.up*offset_y*canvasToWorldFactor+Vector3.right*offset_x*canvasToWorldFactor;
    }

    public void show(string textarg , int speed=-1 , float voicePitch = 0.39f)
    {
	donePrinting = false;
	if (speed == -1)
	    speed = textSpeed;
	gameObject.SetActive(true);
	CurrentText = textarg;
	StartCoroutine(DisplayText(speed, voicePitch));
    }
    
    public void close()
    {
	    gameObject.SetActive(false);
	    StopCoroutine(DisplayText(0, 0));
	    donePrinting=true;
	    hideHint();
    }
    public void hideHint()
    {
	hint.gameObject.SetActive(false);

    }
    public void showHint()
    {
	    hint.gameObject.SetActive(true);
    }
    private IEnumerator DisplayText(int speed, float voicePitch)
    {
        float defaultPitch = dialogue.pitch;
        dialogue.pitch = voicePitch;
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
        dialogue.pitch = defaultPitch;
	    donePrinting = true;
	    yield return new WaitForSecondsRealtime(2f);
	    showHint();
    }
    
}
