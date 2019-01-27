using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPanelController : MonoBehaviour
{
    [SerializeField] GameObject buttonsPanel;

    Animator animator;
    bool isGameOver;

    void Awake() {
        animator = GetComponent<Animator>();
    }

    void Start() {
        EventManager.AddListener("PlayerDie", OnPlayerDie);
        EventManager.AddListener("StartGame", OnStartGame);
    }

    void Update() {
        if (LevelManager.IsGameRunning || isGameOver)
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

    }

    public void OnLeaderboardButtonClick() {
        GameServices.ShowLeaderBoard();
    }
}
