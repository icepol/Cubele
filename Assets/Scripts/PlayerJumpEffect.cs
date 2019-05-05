using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpEffect : MonoBehaviour
{
    [SerializeField] float zoomSpeed = 1f;
    [SerializeField] float opacitySpeed = 0.1f;
    [SerializeField] float colorSpeed = 1.1f;

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

        Color color = spriteRenderer.color;

        // make transparent
        color.a -= opacitySpeed * Time.deltaTime;

        // transform to white
        color.r = Mathf.Min(color.r + ((1f - color.r) * colorSpeed * Time.deltaTime), 1f);
        color.g = Mathf.Min(color.g + ((1f - color.g) * colorSpeed * Time.deltaTime), 1f);
        color.b = Mathf.Min(color.b + ((1f - color.b) * colorSpeed * Time.deltaTime), 1f);

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
