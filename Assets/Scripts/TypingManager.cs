﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class TypingManager : MonoBehaviour
{
    public ThinkingController thinkingController;
    public Sentence sentence;
    public Text currDisplay;
    public Text pointerDisplay;
    public Text removedDisplay;
    private bool goalWasReached = false;

    string pointerSymbol = "|";
    float timeInterval = 0;
    float timeAnimation = 0;

    void setpointer()
    {

	string spaces = "";
	for (int i = 0; i< sentence.currText.Length;i++)
	    spaces +=" ";
	pointerDisplay.text = spaces.Insert(sentence.pointerIndex,pointerSymbol);
	sentence.pointerText = pointerDisplay.text;
    }
    
    void Start()
    {
	
	currDisplay.text = sentence.startText;
	sentence.pointerIndex = sentence.startText.Length;
        setpointer();

    }




    void Update()
    {
        if(!goalWasReached)
            goalWasReached = sentence.isGoalReached(thinkingController);

	    pointerDisplay.gameObject.SetActive(thinkingController.thinkingState);
        currDisplay.gameObject.SetActive(thinkingController.thinkingState);
        removedDisplay.gameObject.SetActive(thinkingController.thinkingState);
	
	
        timeInterval += Time.deltaTime;
        timeAnimation += Time.deltaTime;

        if (timeInterval >= 0.1)
        {
            timeInterval = 0;


            if (thinkingController.getThinkingState())
            {
                if (Input.GetKey(KeyCode.LeftArrow) & sentence.pointerIndex > 0)
                    sentence.pointerIndex--;
                else if (Input.GetKey(KeyCode.RightArrow) & sentence.pointerIndex < sentence.currText.Length)
                    sentence.pointerIndex++;
            }
	        setpointer();
	    }

        if (timeAnimation >= 0.4)
        {
            timeAnimation = 0;

            if (pointerSymbol == "|")
                pointerSymbol = " ";
            else
                pointerSymbol = "|";

        }
	string input = Input.inputString.ToUpper();
	bool at_start =(sentence.pointerIndex == 0);
	bool at_end = (sentence.pointerIndex == sentence.currText.Length);
        if (input.Equals("") | !thinkingController.getThinkingState() |
	    input.Equals(" ") &
	    (
		(at_start || sentence.currText[sentence.pointerIndex-1] == ' ') |// can't put in beginning or before another space
		(!at_end && sentence.currText[sentence.pointerIndex ] ==' ') // can't space after another space
	    )
	)
	{
	    currDisplay.text = sentence.currText;
	    char[] ab = sentence.removedText.ToCharArray();
	    Array.Sort(ab);
	    removedDisplay.text = new string(ab);
	    setpointer();
	    return;
	}
        char c = input[0];
        string curr = sentence.currText;
        string removed = sentence.removedText;
        if ((removed.IndexOf(c) >= 0 & Char.IsLetterOrDigit(c)) | c.Equals((char)32)) //Add a character
        {
            sentence.currText = curr.Insert(sentence.pointerIndex, c.ToString());
            if(!c.Equals((char)32))
                sentence.removedText = removed.Remove(removed.IndexOf(c), 1);

            sentence.pointerIndex++;
        }
        else if (sentence.pointerIndex > 0 & c.Equals((char)8)) //Remove a character
        {
            sentence.pointerIndex--;

            if (!curr[sentence.pointerIndex].Equals((char)32))
                sentence.removedText = removed + curr[sentence.pointerIndex];

            sentence.currText = curr.Remove(sentence.pointerIndex, 1);
	    
        }
        
	currDisplay.text = sentence.currText;
	char[] a = sentence.removedText.ToCharArray();
        Array.Sort(a);
        removedDisplay.text = new string(a);
	setpointer();
    }
    
    public void SetThought (string thought, string solution, UnityEvent solved ,UnityEvent notSolved)
    {
	sentence.startText = thought;
	sentence.currText = thought;
	sentence.removedText = "";
	sentence.goalText = solution;
	sentence.pointerIndex = sentence.startText.Length;
        setpointer();
	sentence.goalNotReached = notSolved;
	sentence.goalReached=solved;
	goalWasReached=false;
    }
}

[System.Serializable]
public class Sentence
{
    public string startText;
    public string currText = "";
    public string removedText = "";
    public string goalText;
    public string pointerText;
    
    public UnityEvent goalReached;
    public UnityEvent goalNotReached;
    public int pointerIndex;

    public bool isGoalReached(ThinkingController thinkingController)
    {
        if (currText.Equals(goalText) & !thinkingController.getThinkingState())
        {
            goalReached.Invoke();
            return true;
        }
        else
            return false;
    }
}
