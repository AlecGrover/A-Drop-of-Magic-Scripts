using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{

    private GameObject _returnObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PassControl(GameObject returnObject)
    {
        _returnObject = returnObject;
    }



    public void OnBack()
    {
        if (_returnObject) _returnObject.SetActive(true);
        gameObject.SetActive(false);
    }
    
}
