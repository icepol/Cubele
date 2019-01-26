using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPanelController : MonoBehaviour
{
    Animator animator;
    bool isGameOver;

    void Awake() {
        animator = GetComponent<Animator>();
    }

    void Start() {
        EventManager.AddListener("PlayerDie", OnPlayerDie);
    }

    void Update() {
        if (LevelManager.IsGameRunning || isGameOver)
            return;

        if (Input.GetMouseButtonDown(0)) {
            animator.SetTrigger("StartGame");

            EventManager.TriggerEvent("StartGame");
        }
    }

    void OnPlayerDie() {
        isGameOver = true;
    }
}
