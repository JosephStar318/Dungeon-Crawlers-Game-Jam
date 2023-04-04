using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CongratsPanelUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup cg;
    [SerializeField] private GameObject panel;

    private void OnEnable()
    {
        GameManager.OnLevelFinished += GameManager_OnLevelFinished;    
    }
    private void OnDisable()
    {
        GameManager.OnLevelFinished -= GameManager_OnLevelFinished;    
    }

    private void GameManager_OnLevelFinished()
    {
        panel.SetActive(true);
        StartCoroutine(cg.FadeIn(0.3f));
    }
}
