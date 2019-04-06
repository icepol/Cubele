using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerupText : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float destroyDelay = 1f;

    [SerializeField] TextMesh powerupText;
    [SerializeField] TextMesh powerupTextShadow;

    Animator animator;

    void Awake() {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        EventManager.AddListener("ResetPowerups", OnResetPowerups);
        EventManager.AddListener("BeforeResetPowerups", OnBeforeResetPowerups);
    }

    void Update()
    {
        transform.Translate(new Vector2(0, moveSpeed * Time.deltaTime));
    }

    void OnDestroy() {
        EventManager.RemoveListener("ResetPowerups", OnResetPowerups);
        EventManager.RemoveListener("BeforeResetPowerups", OnBeforeResetPowerups);
    }

    public void SetText(string text) {
        powerupText.text = "<b>" + text + "</b>\n<size=40>+50</size>";
        powerupTextShadow.text = "<b>" + text + "</b>\n<size=40>+50</size>";
    }

    void OnBeforeResetPowerups() {
        animator.SetTrigger("BeforeResetPowerups");

        Destroy(gameObject, destroyDelay);
    }

    void OnResetPowerups() {
        Destroy(gameObject);
    }
}
