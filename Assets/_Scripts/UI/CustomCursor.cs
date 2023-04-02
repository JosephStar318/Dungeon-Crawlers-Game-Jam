using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomCursor : MonoBehaviour
{
    public static Vector2 Position = Vector2.zero;
    public static Vector3 PositionPerspective = Vector3.zero;

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
        if (PlayerInputHelper.Instance.CurrentControlScheme.Equals("Gamepad"))
        {
            Vector3 dir = PlayerInputHelper.Instance.playerInputActions.Player.Look.ReadValue<Vector2>();
            Vector3 pos = transform.position;
            pos += dir;
            pos.x = Mathf.Clamp(pos.x, 0, Screen.width);
            pos.y = Mathf.Clamp(pos.y, 0, Screen.height);

            transform.position = pos;
        }
        else
        {
            Vector3 pos = Input.mousePosition;
            pos.x = Mathf.Clamp(pos.x, 0, Screen.width);
            pos.y = Mathf.Clamp(pos.y, 0, Screen.height);

            transform.position = pos;
        }
        Position = transform.position;
        PositionPerspective = transform.position;
        PositionPerspective.z = 1f;
    }
}
