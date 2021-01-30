using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeleteItem : MonoBehaviour
{
    public static bool active = false;
    private Button _button;
    private Image _image;

    private void Start()
    {
        _button = GetComponent<Button>();
        _image = GetComponent<Image>();
    }

    private void Update()
    {
        _button.enabled = active;
        _image.enabled = active;
    }
}
