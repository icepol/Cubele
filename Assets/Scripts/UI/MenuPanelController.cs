using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPanelController : MonoBehaviour
{
    Animator animator;

    void Awake() {
        animator = GetComponent<Animator>();
    }

    void Update() {
        if (LevelManager.IsGameRunning)
            return;

        if (Input.GetMouseButtonDown(0)) {
            animator.SetTrigger("StartGame");

            EventManager.TriggerEvent("StartGame");
        }
    }
}
