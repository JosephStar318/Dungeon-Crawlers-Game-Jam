using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI killCountText;

    private PlayerInputHelper playerInputHelper;

    private void Start()
    {
        playerInputHelper = GameObject.FindObjectOfType<PlayerInputHelper>();

        GameManager.OnGameOver += GameManager_OnGameOver;
    }

    private void OnDestroy()
    {
        GameManager.OnGameOver -= GameManager_OnGameOver;
    }
    private void GameManager_OnGameOver()
    {
        killCountText.SetText(GameManager.GetKillCount().ToString());
        gameOverPanel.SetActive(true);
        playerInputHelper.EnableUIActions();
        Time.timeScale = 0f;
    }
}