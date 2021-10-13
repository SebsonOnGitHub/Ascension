using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCThoughtBubbleController : MonoBehaviour
{
    // Start is called before the first frame update
    public float posX;
    public float posY;
    public TMP_Text text;
    private string CurrentText;
    const string kAlphaCode = "<color=#00000000>";
    const float kMaxTextTime = 0.5f;
    public int defaultTextSpeed = 5;
    public float canvasToWorldFactor;
    public PlayerMovement player;
    public NPCJacobMovement jacob;
    private RectTransform rt;
    private Animator anim;
    private int textSpeed;

    void Awake()
    {
	    rt = GetComponent<RectTransform>();
	    anim = GetComponent<Animator>();
    }
    public void ShowStandardText()
    {
	    show("The Most Divine Of Light Comes Not From The Heavens Above, But From Within Us.");
    }
    public void show(string textarg , int speed=-1)
    {
	rt.anchoredPosition = (Vector3.right*posX + Vector3.right*(jacob.transform.position.x - player.transform.position.x) + Vector3.up*posY)*canvasToWorldFactor;
        anim.SetBool("thinking", true);

        if (speed == -1)
            textSpeed = defaultTextSpeed;
        else
            textSpeed = speed;


        
    	CurrentText = textarg;
    }
    
    void FixedUpdate()
    {
	rt.anchoredPosition = (Vector3.right*posX + Vector3.right*(jacob.transform.position.x - player.transform.position.x) + Vector3.up*posY)*canvasToWorldFactor;

	    //transform.Translate( Vector3.zero + player.transform.position);
    }
    public void close()
    {
        StopAllCoroutines();
        anim.SetBool("thinking", false);
    }

    public void startDisplayText()
    {
        StartCoroutine(DisplayText(textSpeed));
    }

    private IEnumerator DisplayText(int speed)
    {
	    string originalText = CurrentText;
	    string displayedText = CurrentText;
	    int alphaIndex = 0;
	    text.text = "";
	    foreach(char c in CurrentText.ToCharArray())
	    {
	        alphaIndex++;
	        text.text = originalText;
	        //if(c != ' ')
                    //AudioController.Dialogue_sound = true;
	        displayedText = text.text.Insert(alphaIndex,kAlphaCode);
	        text.text=displayedText;
	    }

        yield return new WaitForSecondsRealtime(0);
        //AudioController.Dialogue_sound = false;
        NPCJacobMovement.activateGiveHalo();
    }
}
