using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
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
        gameOverPanel.SetActive(true);
        playerInputHelper.EnableUIActions();
        Time.timeScale = 0f;
    }
}