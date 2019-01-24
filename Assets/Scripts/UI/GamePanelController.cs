using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePanelController : MonoBehaviour
{
    void Start() {
        transform.localPosition = new Vector2(-1000, -1000);

        EventManager.AddListener("StartGame", OnStartGame);
    }

    void OnDestroy() {
        EventManager.RemoveListener("StartGame", OnStartGame);
    }

    public void OnStartGame() {
        transform.localPosition = Vector2.zero;
    }
}
