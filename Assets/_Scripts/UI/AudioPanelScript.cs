using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPanelScript : MonoBehaviour
{
    private Action afterHide;
    private void OnEnable()
    {
        PlayerInputHelper.OnBackUI += PlayerInputHelper_OnBackUI;    
    }
    private void OnDisable()
    {
        PlayerInputHelper.OnBackUI -= PlayerInputHelper_OnBackUI;
        Hide();
    }

    private void PlayerInputHelper_OnBackUI()
    {
        Hide();
    }
    public void Show(Action afterHideAction)
    {
        gameObject.SetActive(true);
        afterHide = afterHideAction;
    }
    public void Hide()
    {
        gameObject.SetActive(false);
        afterHide?.Invoke();
    }


}
