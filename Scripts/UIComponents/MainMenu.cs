using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    [Header("New Game Objects")]
    [SerializeField] private int newGameSceneIndex;

    [Header("Continue Menu Objects")]
    [SerializeField] private Button continueButton;
    [SerializeField] private GameObject loadMenu;
    
    [Header("Settings Menu Objects")]
    [SerializeField] private GameObject settingsMenu;

    [Header("Mute Button Objects")]
    [SerializeField] private Button muteButton;
    [SerializeField] private Sprite mutedButton;
    [SerializeField] private Sprite unmutedButton;
    private bool _muted = false;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        if (continueButton) continueButton.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewGame()
    {
        try
        {
            SceneManager.LoadScene(newGameSceneIndex);
        }
        catch (Exception e)
        {
            Debug.Log("Scene Load Failed: " + e.Message);
        }
    }

    public void Continue()
    {
        if (!loadMenu) return;
        loadMenu.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void OnQuit()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }

    public void Settings()
    {
        if (!settingsMenu) return;
        settingsMenu.SetActive(true);
        SettingsMenu settings = settingsMenu.GetComponent<SettingsMenu>();
        if (settings) settings.PassControl(gameObject);
        this.gameObject.SetActive(false);
    }
    
    public void Mute()
    {
        _muted = !_muted;
        if (!muteButton) return;
        muteButton.image.sprite = _muted ? mutedButton : unmutedButton;
    }
    
    
    
}
