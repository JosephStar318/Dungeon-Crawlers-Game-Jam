using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CongratsPanelUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup cg;
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI killCountText;

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
        killCountText.SetText(GameManager.GetKillCount().ToString());
        panel.SetActive(true);
        StartCoroutine(cg.FadeIn(0.3f));
    }
}
