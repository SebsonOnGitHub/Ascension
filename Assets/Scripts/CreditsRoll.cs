using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsRoll : MonoBehaviour
{
    public TextAsset asset;
    private Text credits;
    private Vector3 originalPosition;

    void Awake()
    {
        credits = GetComponent<Text>();
        credits.text = asset.text;
        originalPosition = credits.transform.position;
    }

    private void FixedUpdate()
    {
        credits.transform.position = new Vector3(credits.transform.position.x, credits.transform.position.y + 0.015f, credits.transform.position.z);
    }

    private void OnEnable()
    {
        credits.transform.position = originalPosition;
    }
}
