using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour
{
    public static event Action OnGamePaused;
    public static event Action OnGameUnPaused;

    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private AudioPanelScript audioSettingsPanel;
    [SerializeField] private Button firstBtn;

    private bool isGamePaused = false;
    private PlayerInputHelper playerInputHelper;

    private void OnEnable()
    {
        PlayerInputHelper.OnPause += PlayerInputHelper_OnPause;    
    }
    private void OnDisable()
    {
        PlayerInputHelper.OnPause -= PlayerInputHelper_OnPause;
    }
    private void Start()
    {
        playerInputHelper = GameObject.FindObjectOfType<PlayerInputHelper>();    
    }
    private void PlayerInputHelper_OnPause()
    {

        if(isGamePaused == true)
        {
            UnpauseGame();
        }
        else
        {
            PauseGame();
        }

    }

    private void PauseGame()
    {
        pauseMenuPanel.SetActive(true);
        playerInputHelper.EnableUIActions();
        Time.timeScale = 0f;
        OnGamePaused?.Invoke();
        isGamePaused = true;
    }

    public void UnpauseGame()
    {
        pauseMenuPanel.SetActive(false);
        playerInputHelper.EnablePlayerActions();
        Time.timeScale = 1f;
        OnGameUnPaused?.Invoke();
        isGamePaused = false;
    }

    public void OpenSettings()
    {
        audioSettingsPanel.Show(() => firstBtn.Select());
    }
}
