using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class puzzle_hint_controller : MonoBehaviour
{

    public TMP_Text hintText;
    public SpriteRenderer hintBox;
    private Text keyHint;
    
    // Start is called before the first frame update
    void Start()
    {
	keyHint = GetComponent<Text>();
    }

    void OnDisable()
    {
	hintBox.gameObject.SetActive(false);
    }
    public void showBox (string textarg)
    {
	hintText.text = textarg;
        hintBox.gameObject.SetActive(true);
	hintText.gameObject.SetActive(true);
	keyHint.text = "";
	
    }
    public void closeBox()
    {
	keyHint.text = "press Up for a hint";
	hintBox.gameObject.SetActive(false);
	hintText.gameObject.SetActive(false);
    }
    void FixedUpdate()
    {
	hintBox.gameObject.SetActive(hintText.gameObject.active);
    }
    

}
