using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics;

public class MenuPanelController : MonoBehaviour
{
    [SerializeField] GameObject buttonsPanel;
    [SerializeField] GameObject settingsPanel;

    [SerializeField] Image[] buttonSoundImage;
    [SerializeField] Image[] buttonVibrationsImage;

    Animator animator;
    bool settingsOnScreen;

    void Awake() {
        animator = GetComponent<Animator>();
    }

    void Start() {
        EventManager.AddListener("StartGame", OnStartGame);

        UpdateButtonSound();
        UpdateButtonVibrations();
    }

    void OnStartGame() {
        gameObject.SetActive(false);
    }

    public void OnSettingsButtonClick() {
        settingsOnScreen = !settingsOnScreen;

        if (settingsOnScreen)
            animator.SetTrigger("ShowSettings");
        else
            animator.SetTrigger("HideSettings");
    }

    public void OnSoundsButtonClick() {
        Settings.Sounds = !Settings.Sounds;
        UpdateButtonSound();
    }

    public void OnVibrationsButtonClick() {
        Settings.Vibrations = !Settings.Vibrations;
        UpdateButtonVibrations();
    }

    public void OnPrivacyPolicyButtonClick() {
        AnalyticsEvent.ScreenVisit("PrivacyPolicy");

        Application.OpenURL(Constants.PrivacyPolicyURL);
    }

    public void OnRateButtonClick() {
        AnalyticsEvent.ScreenVisit("Rate");

        Application.OpenURL(Constants.AppStoreLink);
    }

    void UpdateButtonSound() {
        foreach (Image image in buttonSoundImage) {
            Color color = image.color;
            color.a = Settings.Sounds ? 1f : 0.5f;
            image.color = color;
        }
    }

    void UpdateButtonVibrations() {
        foreach (Image image in buttonVibrationsImage) {
            Color color = image.color;
            color.a = Settings.Vibrations ? 1f : 0.5f;
            image.color = color;
        }
    }
}
