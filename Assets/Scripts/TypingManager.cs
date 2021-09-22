using System;
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
	{spaces +=" ";};
        pointerDisplay.text = spaces.Insert(sentence.pointerIndex,pointerSymbol);
	
	sentence.pointerText = pointerDisplay.text;
    }
    
    void Start()
    {

	
        sentence.pointerIndex = sentence.startText.Length;
	
        setpointer();
        //currDisplay.text = sentence.startText.Insert(sentence.pointerIndex, pointerSymbol);
	//pointerDisplay.text = sentence.pointerText.insert(sentence.pointerIndex,pointerSymbol);
    }

    void Update()
    {
        if(!goalWasReached)
            goalWasReached = sentence.isGoalReached(thinkingController);

        currDisplay.gameObject.SetActive(thinkingController.showThought);
        removedDisplay.gameObject.SetActive(thinkingController.showThought);
	pointerDisplay.gameObject.SetActive(thinkingController.showThought);
	
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

        if (input.Equals("") | !thinkingController.getThinkingState())
            return;

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
	
        string spaces = "";
	for (int i = 0; i< sentence.currText.Length;i++)
	{spaces +=" ";}
	//spaces.Insert(sentence.pointerIndex,pointerSymbol);
        pointerDisplay.text = spaces.Insert(sentence.pointerIndex,pointerSymbol);
	sentence.pointerText = pointerDisplay.text;
        
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
            PlayerMovement.toggleMovable(true);
            goalReached.Invoke();
            return true;
        }
        else
            return false;
    }
}
