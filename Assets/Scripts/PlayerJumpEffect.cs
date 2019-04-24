using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpEffect : MonoBehaviour
{
    [SerializeField] float zoomSpeed = 1f;
    [SerializeField] float opacitySpeed = 0.1f;

    SpriteRenderer spriteRenderer;
    bool isActive = true;

    void Awake() {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Start() {
        EventManager.AddListener("PlayerDie", OnPlayerDie);
    }

    void Update() {
        if (!isActive)
            return;

        // zoom
        transform.localScale = transform.localScale * (1 + zoomSpeed * Time.deltaTime);

        // make transparent
        Color color = spriteRenderer.color;
        color.a -= opacitySpeed;
        spriteRenderer.color = color;
    }

    void OnDestroy() {
        // stop moving
        EventManager.RemoveListener("PlayerDie", OnPlayerDie);
    }

    void OnPlayerDie() {
        isActive = false;
    }

    public void SetRotation(Quaternion rotation) {
        spriteRenderer.transform.localRotation = rotation;
    }
}
