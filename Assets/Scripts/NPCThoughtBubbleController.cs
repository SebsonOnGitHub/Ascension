﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCThoughtBubbleController : MonoBehaviour
{
    // Start is called before the first frame update
    public float offset_x;
    public TMP_Text text;
    private string CurrentText;
    const string kAlphaCode = "<color=#00000000>";
    const float kMaxTextTime = 0.5f;
    public int textSpeed = 5;
    const float canvasToWorldFactor = 65.0f;
    public PlayerMovement player;
    public NPCJacobMovement jacob;
    private RectTransform rt;
    void Start()
    {
	rt = GetComponent<RectTransform>();
        gameObject.SetActive(false);
    }
    public void ShowStandardText()
    {
	show("here i give u halo!!!");
    }
    public void show(string textarg , int speed=-1 ,float offset_x = 0)
    {
	if (speed == -1)
	    speed = textSpeed;
	rt.anchoredPosition = Vector3.right*( jacob.transform.position.x - player.transform.position.x+ offset_x)*canvasToWorldFactor;
        gameObject.SetActive(true);
	CurrentText = textarg;
	StartCoroutine(DisplayText(speed));
    }
    void Update()
    {
	rt.anchoredPosition = Vector3.right*( jacob.transform.position.x - player.transform.position.x+ offset_x)*canvasToWorldFactor;
	//transform.Translate( Vector3.zero + player.transform.position);
    }
    public void close()
    {
	gameObject.SetActive(false);
	StopAllCoroutines();
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