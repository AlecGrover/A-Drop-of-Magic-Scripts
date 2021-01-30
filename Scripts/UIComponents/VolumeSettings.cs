using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private TextMeshProUGUI volumeLabel;
    [SerializeField] private string volumeText = "Volume: ";
    private int _volume;

    [SerializeField] private AudioMixer audioMixer;
    
    // Start is called before the first frame update
    void Start()
    {
        if (!volumeSlider) return;
        _volume = Mathf.FloorToInt(PlayerPrefs.GetFloat("MasterVolume", 75));
        if (volumeLabel) volumeLabel.text = volumeText + _volume;
        volumeSlider.value = _volume;
    }
    
    public void ChangeVolume(float volume)
    {
        _volume = Mathf.FloorToInt(volume);
        PlayerPrefs.SetFloat("MasterVolume", _volume);
        if (volumeLabel) volumeLabel.text = volumeText + _volume;
        float usedVolume;
        if (_volume >= 70)
        {
            usedVolume = 11.5816f * Mathf.Sin(2.66184f * _volume/100f +4.05294f)+5.15515f;
        }
        else
        {
            usedVolume = _volume / 70f;
        }
        
        var setVolume = Mathf.Log10(Mathf.Clamp(usedVolume, 0.0001f, 10)) * 20;
        audioMixer.SetFloat("volume", setVolume);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
