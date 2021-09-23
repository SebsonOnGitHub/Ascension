using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsRoll : MonoBehaviour
{
    public TextAsset asset;
    private Text credits;

    void Start()
    {
        credits = GetComponent<Text>();
        credits.text = asset.text;
    }

    private void FixedUpdate()
    {
        credits.transform.position = new Vector3(credits.transform.position.x, credits.transform.position.y + 0.01f, credits.transform.position.z);
    }
}
