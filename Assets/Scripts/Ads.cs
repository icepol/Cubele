using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.Advertisements;

public class Ads {

    public enum AdProvider {
        GOOGLE,
        UNITY,
    }

    static InterstitialAd interstitial;
    static RewardBasedVideoAd rewards;

    static bool isInitialized;

    public static AdProvider LastDisplayedAd { get; private set; }
    public static AdProvider LastRequestedAd { get; private set; }

    // Use this for initialization
    public static void Initialize() {
        if (isInitialized)
            return;

        if (Constants.IsGoogleAdEnabled)
            MobileAds.Initialize(Constants.AdmobAppId);

        if (Constants.IsUnityAdEnabled)
            Advertisement.Initialize(Constants.GameID);

        isInitialized = true;
    }

    #region INTERSTITIAL
    public static void RequestInterstitial(string InterstitialAdId) {
        Initialize();

        interstitial = new InterstitialAd(InterstitialAdId);
        interstitial.OnAdLoaded += OnLoaded;
        interstitial.OnAdFailedToLoad += OnLoadFailed;
        interstitial.OnAdOpening += OnOpening;
        interstitial.OnAdClosed += OnClose;

        AdRequest request = new AdRequest.Builder().Build();

        interstitial.LoadAd(request);
    }

    public static bool IsInterstitialLoaded() {
        return interstitial != null && interstitial.IsLoaded();
    }

    public static void ShowInterstitial() {
        LastRequestedAd = AdProvider.GOOGLE;

        if (interstitial != null && interstitial.IsLoaded()) {
            interstitial.Show();
            LastDisplayedAd = AdProvider.GOOGLE;
        }
    }

    public static void DestroyInterstitial() {
        if (interstitial != null) {
            interstitial.Destroy();
        }
    }
    #endregion

    #region REWARDS
    static RewardBasedVideoAd RewardsAds() {
        Initialize();

        if (rewards == null) {
            rewards = RewardBasedVideoAd.Instance;
            rewards.OnAdLoaded += OnRewardLoaded;
            rewards.OnAdFailedToLoad += OnRewardLoadFailed;
            rewards.OnAdOpening += OnRewardOpening;
            rewards.OnAdClosed += OnRewardClose;
            rewards.OnAdRewarded += OnRewarded;
        }

        return rewards;
    }

    public static void RequestRewards(string RewardsAdId) {
        AdRequest request = new AdRequest.Builder().Build();
        RewardsAds().LoadAd(request, RewardsAdId);
    }

    public static bool IsRewardsLoaded() {
        return RewardsAds().IsLoaded();
    }

    public static void ShowRewards() {
        if (RewardsAds().IsLoaded()) {
            RewardsAds().Show();
        }
    }
    #endregion

    #region CALLBACK
    static void OnLoaded(object sender, EventArgs e) {
        Debug.Log("Ad loaded");
    }

    static void OnLoadFailed(object sender, AdFailedToLoadEventArgs e) {
        Debug.Log("Ad load failed: " + e.Message);
    }

    static void OnOpening(object sender, EventArgs e) {
        Debug.Log("Ad opening");
    }

    static void OnClose(object sender, EventArgs e) {
        Debug.Log("Ad closing");
    }

    static void OnRewardLoaded(object sender, EventArgs e) {
        Debug.Log("Reward Ad loaded");
    }

    static void OnRewardLoadFailed(object sender, AdFailedToLoadEventArgs e) {
        Debug.Log("Reward Ad load failed: " + e.Message);
    }

    static void OnRewardOpening(object sender, EventArgs e) {
        Debug.Log("Reward Ad opening");
    }

    static void OnRewardClose(object sender, EventArgs e) {
        Debug.Log("Reward Ad closing");
        EventManager.TriggerEvent("AdNotRewarded");
    }

    static void OnRewarded(object sender, Reward e) {
        Debug.Log(String.Format("Ad rewarded -> type: {0}, amount: {1}", e.Type, e.Amount));
        EventManager.TriggerEvent("AdRewarded");
    }
    #endregion

    #region UNITY
    public static void ShowUnityAd(string placementId) {
        LastRequestedAd = AdProvider.UNITY;

        if (Advertisement.IsReady(placementId)) {
            Advertisement.Show(placementId);
            LastDisplayedAd = AdProvider.UNITY;
        }
    }
    #endregion
}
