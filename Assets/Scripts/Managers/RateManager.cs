using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;

public class RateManager : MonoBehaviour {

    [SerializeField] GameObject ratePanel;
    [SerializeField] int maxShowForm = 3;
    [SerializeField] int skipShowForm = 5;
    [SerializeField] string nextScreenName = "Menu";

    static bool isOnScreen;

    void Start() {
        #if !UNITY_WEBGL
        isOnScreen = false;
        ratePanel.SetActive(false);

        if (Settings.RateFormTotalCount > 3 || Settings.RateFormUsed || Time.time < 30f) {
            // do noting if user don't readct 3 times or if game is running too short
            return;
        }

        Settings.RateFormRequestCount++;
        if (Settings.RateFormRequestCount > 5) {
            Settings.RateFormRequestCount = 0;
            ShowPanel();
        }
        #endif
    }

    public static bool IsOnScreen {
        get { return isOnScreen; }
    }

    public void ShowPanel() {
        Settings.RateFormTotalCount++;
        ratePanel.SetActive(true);
        isOnScreen = true;

        AnalyticsEvent.ScreenVisit("Rate");
    }

    public void YesButtonClick() {
        ratePanel.SetActive(false);
        isOnScreen = false;
        Settings.RateFormUsed = true;

        if (nextScreenName != "")
            SceneManager.LoadScene(nextScreenName);

        Application.OpenURL(Constants.AppStoreLink);
    }

    public void LaterButtonClick() {
        ratePanel.SetActive(false);
        isOnScreen = false;

        if (nextScreenName != "")
            SceneManager.LoadScene(nextScreenName);
    }
}
