using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

    public static bool gamePaused = false;
    
    [Header("Main Menu Objects")]
    [SerializeField] private int mainMenuSceneIndex;
    
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnResume()
    {
        Resume();
        gameObject.SetActive(false);
    }

    private static void Resume()
    {
        gamePaused = false;
        Time.timeScale = 1f;
    }

    public void OnMainMenu()
    {
        try
        {
            Resume();
            SceneManager.LoadScene(mainMenuSceneIndex);
        }
        catch (Exception e)
        {
            Debug.Log("Scene Load Failed: " + e.Message);
        }
    }
    
    public void OnQuit()
    {
        Debug.Log("Quitting Game");
        Resume();
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
