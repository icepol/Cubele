using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] Text topScoreText;
    [SerializeField] Text scoreText;

    bool isDead;
    float score;

    void Start() {
        EventManager.AddListener("PlayerDie", OnPlayerDie);

        topScoreText.text = Settings.TopScore.ToString();
    }

    void FixedUpdate() {
        if (!LevelManager.IsGameRunning)
            return;

        if (isDead)
            return;

        PlayerStats.PlayTime += Time.deltaTime;

        scoreText.text = PlayerStats.Score.ToString();
    }

    void OnDestroy() {
        EventManager.RemoveListener("PlayerDie", OnPlayerDie);
    }

    void OnPlayerDie() {
        // stop counting score
        isDead = true;

        if (PlayerStats.Score > Settings.TopScore) {
            Settings.TopScore = PlayerStats.Score;
            PlayerStats.IsNewTop = true;
        }

        if (PlayerStats.Distance > Settings.TopDistance)
            Settings.TopDistance = PlayerStats.Distance;

        GameServices.ReportScore(Constants.TopScoreLeaderBoardId, PlayerStats.Score);
        GameServices.ReportScore(Constants.TopDistanceReachedId, (int)(PlayerStats.Distance * 100));
        GameServices.ReportScore(Constants.BonusCollectorId, PlayerStats.BonusCount);
        GameServices.ReportScore(Constants.LongestGameplayId, (int)(PlayerStats.PlayTime * 100));
    }
}
