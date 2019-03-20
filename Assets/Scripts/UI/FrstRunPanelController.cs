using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrstRunPanelController : MonoBehaviour
{
    [SerializeField] Text cubeleText;
    [SerializeField] GameObject tapToJumpText;

    Animator animator;

    bool isGameStarted;

    void Awake() {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        if (PlayerStats.ShowGameOverPanel) {
            // keep this panel turned off
            gameObject.SetActive(false);
            return;
        }

        EventManager.AddListener("StartGame", OnStartGame);
    }

    void OnDestroy() {
        EventManager.RemoveListener("StartGame", OnStartGame);
    }

    void LateUpdate() {
        if (isGameStarted)
            return;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
            if (Camera.main.ScreenToWorldPoint(Input.mousePosition).y < 0f) {
                EventManager.TriggerEvent("StartGame");
            }
        }
    }

    void OnStartGame() {
        isGameStarted = true;
        tapToJumpText.SetActive(false);

        animator.SetTrigger("StartGame");
    }
}
