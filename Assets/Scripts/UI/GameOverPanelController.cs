using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Analytics;

public class GameOverPanelController : MonoBehaviour
{
    [SerializeField] Text topScore;
    [SerializeField] Text yourScore;
    [SerializeField] Text newTopLabel;

    [SerializeField] Image[] buttonSoundImages;

    SoundsManager soundsManager;

    bool isAdShown;

    void Awake() {
        soundsManager = FindObjectOfType<SoundsManager>();
    }

    void Start() {
        if (PlayerStats.ShowGameOverPanel) {
            // show game over panel
            topScore.text = Settings.TopScore.ToString();
            yourScore.text = PlayerStats.Score.ToString();

            newTopLabel.enabled = PlayerStats.IsNewTop;

            if (PlayerStats.IsNewTop)
                soundsManager.NewTopScore();
            else
                // show ad only if there is no new score reached
                ShowAd();

            // move panel on screen
            transform.localPosition = Vector2.zero;
        }
        else {
            // remove game over panel off the screen
            transform.localPosition = new Vector2(-1000, -1000);
        }

        EventManager.AddListener("StartGame", OnStartGame);

        UpdateButtonSound();
    }

    void OnDestroy() {
        EventManager.RemoveListener("StartGame", OnStartGame);
    }

    void OnStartGame() {
        // remove game panel off the screen
        transform.localPosition = new Vector2(-1000, -1000);

        if (isAdShown) {
            Ads.DestroyInterstitial();
        }
    }

    void ShowAd() {
        if (PlayerStats.GamePlayCount % 2 == 0) {
            isAdShown = true;
            Ads.ShowInterstitial();
        }
    }

    public void OnSoundsButtonClick() {
        Settings.Sounds = !Settings.Sounds;
        UpdateButtonSound();
    }

    public void OnPrivacyPolicyButtonClick() {
        AnalyticsEvent.ScreenVisit("PrivacyPolicy");

        Application.OpenURL(Constants.PrivacyPolicyURL);
    }

    public void OnLeaderboardButtonClick() {
        AnalyticsEvent.ScreenVisit("LeaderBoard");
        
        GameServices.ShowLeaderBoard();
    }

    public void OnReplayButtonClick() {
        EventManager.TriggerEvent("StartGame");
    }

    public void OnShareButtonClick() {
        AnalyticsEvent.ScreenVisit("Share");

        string filePath = Path.Combine(Application.streamingAssetsPath, "Cubele.png");

        new NativeShare().AddFile(filePath).SetSubject(
            "Beat my score in #cubele"
        ).SetText(
            "I scored " + PlayerStats.Score.ToString() + " points in #cubele\nCan you beat my score?\n" + Constants.ShareLink
        ).Share();
    }

    void UpdateButtonSound() {
        foreach (Image image in buttonSoundImages) {
            Color color = image.color;
            color.a = Settings.Sounds ? 1f : 0.5f;
            image.color = color;
        }
    }
}
