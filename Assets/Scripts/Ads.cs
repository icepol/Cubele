using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class Ads : MonoBehaviour {

    public delegate void OnAdRewarded();
    public delegate void OnAdNotRewarded();

    public static event OnAdRewarded AdRewarded;
    public static event OnAdNotRewarded AdNotRewarded;

    static InterstitialAd interstitial;
    static RewardBasedVideoAd rewards;

    // Use this for initialization
    static void Start() {
        // MobileAds.Initialize(Constants.AdmobAppId);
    }

    #region INTERSTITIAL
    public static void RequestInterstitial(string InterstitialAdId) {
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
        if (interstitial != null && interstitial.IsLoaded()) {
            interstitial.Show();
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
        print("Ad loaded");
    }

    static void OnLoadFailed(object sender, AdFailedToLoadEventArgs e) {
        print("Ad load failed: " + e.Message);
    }

    static void OnOpening(object sender, EventArgs e) {
        print("Ad opening");
    }

    static void OnClose(object sender, EventArgs e) {
        print("Ad closing");
    }

    static void OnRewardLoaded(object sender, EventArgs e) {
        print("Reward Ad loaded");
    }

    static void OnRewardLoadFailed(object sender, AdFailedToLoadEventArgs e) {
        print("Reward Ad load failed: " + e.Message);
    }

    static void OnRewardOpening(object sender, EventArgs e) {
        print("Reward Ad opening");
    }

    static void OnRewardClose(object sender, EventArgs e) {
        print("Reward Ad closing");

        AdNotRewarded();
    }

    static void OnRewarded(object sender, Reward e) {
        print(String.Format("Ad rewarded -> type: {0}, amount: {1}", e.Type, e.Amount));

        AdRewarded();
    }
    #endregion
}
