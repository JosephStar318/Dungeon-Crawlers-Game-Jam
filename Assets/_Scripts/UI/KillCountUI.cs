using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KillCountUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI killCountText;

    private void OnEnable()
    {
        GameManager.OnKillCountChanged += GameManager_OnKillCountChanged;
    }
    private void OnDisable()
    {
        GameManager.OnKillCountChanged -= GameManager_OnKillCountChanged;
    }

    private void GameManager_OnKillCountChanged(int val)
    {
        killCountText.SetText($"Enemy Kills: {val}");
    }
}
