using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class GameOverManager : MonoBehaviour
{
    void Start()
    {
        Ads.ShowInterstitial();

        AnalyticsEvent.ScreenVisit("GameOver");
    }

    void OnDestroy()
    {
        Ads.DestroyInterstitial();
    }
}
