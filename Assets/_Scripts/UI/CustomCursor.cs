using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomCursor : MonoBehaviour
{
    void Start()
    {
        HideCursor();
    }

    private void HideCursor()
    {
        Cursor.visible = false;
    }
    private void ShowCursor()
    {
        Cursor.visible = true;
    }

    void Update()
    {
        transform.position = Input.mousePosition;
    }
}
