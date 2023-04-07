using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LevelSelectionUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup cg;
    
    private Action AfterHideFunc;

    private void OnEnable() => PlayerInputHelper.OnBackUI += PlayerInputHelper_OnBackUI;
    private void OnDisable() => PlayerInputHelper.OnBackUI -= PlayerInputHelper_OnBackUI;
    public void PlayerInputHelper_OnBackUI()
    {
        if (PlayerInputHelper.Instance.IsUIActionsEnabled())
        {
            Hide();
        }
    }
    public void Hide()
    {
        StartCoroutine(cg.FadeOut(0.1f,() =>{
            gameObject.SetActive(false);
            AfterHideFunc?.Invoke();
        }));
    }
    public void Show(Action action)
    {
        AfterHideFunc = action;
        gameObject.SetActive(true);
        StartCoroutine(cg.FadeIn(0.1f));
    }

    public void LoadLevel(string levelName)
    {
        if(string.IsNullOrEmpty(levelName) == false)
        {
            GameManager.LoadGame(levelName);
        }
    }

    

}
