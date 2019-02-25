using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class LevelManager : MonoBehaviour
{
    void Start() {
        Application.targetFrameRate = 60;

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
        PlayerStats.ResetAll();
        PlayerStats.GamePlayCount++;

        AnalyticsEvent.GameStart();
        IsGameRunning = true;
    }

    void OnPlayerDie() {
        AnalyticsEvent.GameOver();
        IsGameRunning = false;

        if (PlayerStats.GamePlayCount % 2 == 0)
            // show ad each second game
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
