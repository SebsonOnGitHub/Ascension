using System.Collections;
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
    public int defaultTextSpeed = 5;
    const float canvasToWorldFactor = 65.0f;
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
	    show("asdasdhere i give u halo!!!");
    }
    public void show(string textarg , int speed=-1 ,float offset_x = 0)
    {
        anim.SetBool("thinking", true);

        if (speed == -1)
            textSpeed = defaultTextSpeed;
        else
            textSpeed = speed;

        rt.anchoredPosition = Vector3.right*( jacob.transform.position.x - player.transform.position.x+ offset_x)*canvasToWorldFactor;
    	CurrentText = textarg;
    }
    void Update()
    {
	    rt.anchoredPosition = Vector3.right*( jacob.transform.position.x - player.transform.position.x+ offset_x)*canvasToWorldFactor;
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
        player.ToggleHalo();
    }
}
