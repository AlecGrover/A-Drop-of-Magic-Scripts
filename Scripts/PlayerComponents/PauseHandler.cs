using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseHandler : MonoBehaviour
{
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject pauseMenu;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnPause()
    {
        PauseMenu.gamePaused = !PauseMenu.gamePaused;
        Time.timeScale = !PauseMenu.gamePaused ? 1f : 0f;
        if (pauseMenu) pauseMenu.SetActive(PauseMenu.gamePaused);
        if (settingsMenu) settingsMenu.SetActive(false);
    }
    
}
