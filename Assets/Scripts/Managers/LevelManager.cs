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
        EventManager.AddListener("PlayerDie", OnPlayerDie);
        EventManager.AddListener("BonusCollision", OnBonusCollision);

        Ads.Initialize();
        GameServices.Initialize();

        AnalyticsEvent.ScreenVisit("Game");
    }

    void OnDestroy() {
        EventManager.RemoveListener("StartGame", OnStartGame);
        EventManager.RemoveListener("PlayerDie", OnPlayerDie);
        EventManager.RemoveListener("BonusCollision", OnBonusCollision);
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

        Ads.RequestInterstitial(PlayerStats.PlayTime > 10f ? Constants.GameOverVideoId : Constants.GameOverSimpleId);
    }

    void OnBonusCollision() {
        StartCoroutine(SlowMotion());
    }

    IEnumerator SlowMotion() {
        // slow down and wait 1s
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(0.2f);

        Time.timeScale = 1f;
    }
}
