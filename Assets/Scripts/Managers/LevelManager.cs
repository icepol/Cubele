using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class LevelManager : MonoBehaviour
{
    void Start() {
        PlayerStats.ResetAll();

        IsGameRunning = false;

        EventManager.AddListener("StartGame", OnStartGame);
        EventManager.AddListener("PlaterDie", OnPlayerDie);
    }

    void OnDestroy() {
        EventManager.RemoveListener("StartGame", OnStartGame);
        EventManager.RemoveListener("PlayerDie", OnPlayerDie);
    }

    public static bool IsGameRunning {
        get; set;
    }

    void OnStartGame() {
        AnalyticsEvent.GameStart();
        IsGameRunning = true;
    }

    void OnPlayerDie() {
        AnalyticsEvent.GameOver();
        IsGameRunning = false;
    }
}
