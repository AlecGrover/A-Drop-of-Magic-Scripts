using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSetup : MonoBehaviour
{

    [SerializeField] private UIController uiController;
    [SerializeField] private bool startActiveMainMenu = false;
    [SerializeField] private bool startActiveSettingsMenu = false;
    [SerializeField] private bool startActivePauseMenu = false;
    // ReSharper disable once InconsistentNaming
    [SerializeField] private bool startActiveHUD = false;
    [SerializeField] private bool startActiveCraftingMenu = false;

    private void Awake()
    {
        if (!uiController) return;
        uiController = Instantiate(uiController);
    }


    // Start is called before the first frame update
    void Start()
    {
        if (!uiController) return;
        uiController.mainMenu.gameObject.SetActive(startActiveMainMenu);
        uiController.settingsMenu.gameObject.SetActive(startActiveSettingsMenu);
        uiController.pauseMenu.gameObject.SetActive(startActivePauseMenu);
        uiController.hud.gameObject.SetActive(startActiveHUD);
        uiController.craftingMenu.gameObject.SetActive(startActiveCraftingMenu);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
