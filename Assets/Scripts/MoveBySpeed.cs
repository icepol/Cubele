using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBySpeed : MonoBehaviour
{
    [SerializeField] float speed = 1f;

    bool isActive = true;

    void Start() {
        EventManager.AddListener("PlayerDie", OnPlayerDie);
    }

    void Update() {
        if (!isActive)
            return;

        // move down by speed
        transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0));
    }

    void OnDestroy() {
        // stop moving
        EventManager.RemoveListener("PlayerDie", OnPlayerDie);
    }

    void OnPlayerDie() {
        isActive = false;
    }
}
