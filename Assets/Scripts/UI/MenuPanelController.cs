using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPanelController : MonoBehaviour
{
    [SerializeField] GameObject buttonsPanel;
    [SerializeField] Image buttonSoundImage;

    Animator animator;
    bool isGameOver;

    void Awake() {
        animator = GetComponent<Animator>();
    }

    void Start() {
        EventManager.AddListener("PlayerDie", OnPlayerDie);
        EventManager.AddListener("StartGame", OnStartGame);

        UpdateButtonSound();
    }

    void Update() {
        if (LevelManager.IsGameRunning || isGameOver || RateManager.IsOnScreen)
            return;

        if (Input.GetMouseButtonDown(0)) {
            // start game on tap to screen
            if (Camera.main.ScreenToWorldPoint(Input.mousePosition).y < 3.4f) {
                // first part contains buttons
                animator.SetTrigger("StartGame");

                EventManager.TriggerEvent("StartGame");
            }
        }
    }

    void OnPlayerDie() {
        isGameOver = true;
    }

    void OnStartGame() {
        buttonsPanel.SetActive(false);
    }

    public void OnSoundsButtonClick() {
        Settings.Sounds = !Settings.Sounds;
        UpdateButtonSound();
    }

    public void OnLeaderboardButtonClick() {
        GameServices.ShowLeaderBoard();
    }

    void UpdateButtonSound() {
        Color color = buttonSoundImage.color;
        color.a = Settings.Sounds ? 1f : 0.5f;
        buttonSoundImage.color = color;
    }
}
