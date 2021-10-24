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
    public Text hintDisplay;
    private KeyCode prevKey;
    string pointerSymbol = "|";
    float timeInterval = 0;
    float timeAnimation = 0;
    public float hintWaitTime;
    private float hintWait;
    private string hintText;
    public puzzle_hint_controller puzzleHint;

    void setpointer()
    {
	if(sentence.currText.Length == 0)
	{
	    sentence.pointerText = pointerSymbol;
	    return;
	}
	string spaces = "";
	for (int i = 0; i< sentence.currText.Length;i++)
	    spaces +=" ";
	sentence.pointerText = spaces.Insert(sentence.pointerIndex,pointerSymbol);
    }
    void updateDisplay()
    {
	currDisplay.text = sentence.currText;
	char[] ab = sentence.removedText.ToCharArray();
	Array.Sort(ab);
	removedDisplay.text = new string(ab);
	setpointer();
	pointerDisplay.text = sentence.pointerText;
    }
    
    void Start()
    {
	hintWait = hintWaitTime;
	currDisplay.text = sentence.startText;
	sentence.pointerIndex = sentence.startText.Length;
	hintText = "If only you could move...";
	setpointer();
    }

    void Update() //fixedUpdate doesn't work here
    {
	if(sentence.isGoalReached(thinkingController))
	{
	    updateDisplay();
	    resetHint(hintWaitTime);
	    return;
	}
        timeInterval += Time.deltaTime;
        timeAnimation += Time.deltaTime;
	
	currDisplay.gameObject.SetActive(thinkingController.isThinkingIdle());
        removedDisplay.gameObject.SetActive(thinkingController.isThinkingIdle());
	pointerDisplay.gameObject.SetActive(thinkingController.isThinkingIdle() && (timeAnimation >= 0.4) && (sentence.currText != "" || sentence.removedText != ""));
	hintDisplay.gameObject.SetActive(thinkingController.isThinkingIdle() && hintWait <=0);
	
	if (timeAnimation >=0.8)
	    timeAnimation =0;
	timeInterval = 0;

	if (thinkingController.isThinkingIdle() && prevKey == KeyCode.None)
	{
	    if(sentence.currText != "" || sentence.removedText!="")
			hintWait -= Time.deltaTime;
	    if (Input.GetKey(KeyCode.LeftArrow) & sentence.pointerIndex > 0)
	    {
		sentence.pointerIndex--;
		prevKey = KeyCode.LeftArrow;
	    }
	    else if (Input.GetKey(KeyCode.RightArrow) & sentence.pointerIndex < sentence.currText.Length)
	    {
		sentence.pointerIndex++;
		prevKey = KeyCode.RightArrow;
	    }
	    else if(Input.GetKey(KeyCode.Delete) &sentence.pointerIndex < sentence.currText.Length)
	    {
		//delete following character
		prevKey = KeyCode.Delete;
		if (Char.IsLetterOrDigit(sentence.currText[sentence.pointerIndex]))
		    sentence.removedText = sentence.removedText + sentence.currText[sentence.pointerIndex];
		sentence.currText = sentence.currText.Remove(sentence.pointerIndex, 1);
	    }
	    else if (Input.GetKey(KeyCode.UpArrow) && hintWait<=0)
	    {
		prevKey = KeyCode.UpArrow;
		puzzleHint.showBox(hintText);
	    }
	}
	if (Input.GetKeyUp(prevKey))
	    prevKey= KeyCode.None;
	
	string input = Input.inputString.ToUpper();
	bool at_start =(sentence.pointerIndex == 0);
	bool at_end = (sentence.pointerIndex == sentence.currText.Length);
	bool noValidInput = input.Equals("") | !thinkingController.isThinkingIdle() | input.Equals(" ") &
	    (
		(at_start || sentence.currText[sentence.pointerIndex-1] == ' ')// can't put in beginning or before another space
		| (!at_end && sentence.currText[sentence.pointerIndex ] ==' ') // can't space after another space
	    );
	    
	if (!noValidInput)
	{
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
			if (Char.IsLetterOrDigit(curr[sentence.pointerIndex]))
				sentence.removedText = removed + curr[sentence.pointerIndex];
			sentence.currText = curr.Remove(sentence.pointerIndex, 1);
	    }
	}
	updateDisplay();
    }
    
    public void SetThought (string thought, List<string> solution, UnityEvent solved ,UnityEvent notSolved, string hint = "")
    {
	sentence.startText = thought;
	sentence.currText = thought;
	sentence.removedText = "";
	sentence.goalText = solution;
	sentence.pointerIndex = sentence.startText.Length;
	updateDisplay();
	sentence.goalNotReached = notSolved;
	sentence.goalReached=solved;
	resetHint(hintWaitTime);
	hintText = hint;
    }

    public void resetHint(float arg)
    {
	hintWait  = arg;
	puzzleHint.closeBox();
	
    }
    public void addSolution(string solution)
    {
	sentence.goalText.Add(solution);
	
    }
}

[System.Serializable]
public class Sentence
{
    public string startText;
    public string currText = "";
    public string removedText = "";
    public List<string> goalText;
    public string pointerText;
    
    public UnityEvent goalReached;
    public UnityEvent goalNotReached;
    public int pointerIndex;

    public bool isGoalReached(ThinkingController thinkingController)
    {
	bool isReached = false;
	foreach (string solve in goalText)
	{
	    if(currText.Equals(solve))
		isReached = true;
	}
	
        if (isReached & !thinkingController.getThinkingState())
        {
	    currText="";
	    removedText="";
	    pointerText="";
	    pointerIndex=0;
	    goalReached.Invoke();

	    return true;
        }
        else
            return false;
    }
}
