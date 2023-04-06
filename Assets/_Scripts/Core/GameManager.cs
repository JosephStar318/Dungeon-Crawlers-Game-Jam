using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static event Action OnGameOver;
    public static event Action<int> OnKillCountChanged;
    public static event Action OnLevelFinished;

    private Player player;
    private MonsterSpawner monsterSpawner;

    private const int spawnRadius = 3;
    private const int SegmentLength = 8;

    [SerializeField] private int spawnProbability = 30;
    
    private static int killCount;

    private void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
        monsterSpawner = GetComponent<MonsterSpawner>();
        killCount = 0;
        player.GetComponent<Health>().OnDead += Player_OnDead;
        InvokeRepeating(nameof(SpawnMonsters), 10f, 10f);
    }
    private void OnEnable()
    {
        Monster.OnAnyMonsterKilled += Monster_OnAnyMonsterKilled;
    }
    private void OnDisable()
    {
        Monster.OnAnyMonsterKilled -= Monster_OnAnyMonsterKilled;
    }
    private void Monster_OnAnyMonsterKilled()
    {
        killCount++;
        OnKillCountChanged?.Invoke(killCount);
    }

    public static void QuitGame()
    {
        Application.Quit();
    }

    public static void ReLoadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }
    public static void LoadGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1f;
    }

    public static void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }

    public static void LevelComplete()
    {
        Time.timeScale = 0;
        PlayerInputHelper.Instance.EnableUIActions();
        OnLevelFinished?.Invoke();
    }
    public static int GetKillCount()
    {
        return killCount;
    }
    private void Player_OnDead(Vector3 obj)
    {
        OnGameOver?.Invoke();
    }

    private void SpawnMonsters()
    {
        int probablity = UnityEngine.Random.Range(0, 100);
        DungeonSegment segment = player.GetCurrentSegment();
        if (probablity < spawnProbability && segment != null)
        {
            monsterSpawner.SpawnMonsters(segment, SegmentLength, spawnRadius);
        }
    }


}
