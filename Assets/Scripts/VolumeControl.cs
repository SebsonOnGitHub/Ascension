using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeControl : MonoBehaviour
{
    public string volumeParameter = "MasterVolume";
    public AudioMixer mixer;
    public UnityEngine.UI.Slider slider;
    private float multiplier = 30f;

    void Awake()
    {
        slider.onValueChanged.AddListener(HandleSliderValueChanged);
    }

    private void HandleSliderValueChanged(float value)
    {
        mixer.SetFloat(volumeParameter, Mathf.Log10(value) * multiplier);
    }
}
