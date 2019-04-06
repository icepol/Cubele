using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerup : MonoBehaviour
{
    [SerializeField] float powerupDuration = 5f;

    float defaultScale;
    [SerializeField] float minimizedScale = 0.4f;

    float defaultGravity;
    [SerializeField] float powerupGravity = 1.6f;

    Coroutine powerupCoroutine;

    Rigidbody2D rb2d;
    Animator animator;
    Player player;
    SoundsManager soundsManager;

    void Awake() {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GetComponent<Player>();

        soundsManager = FindObjectOfType<SoundsManager>();
    }

    void Start()
    {
        // store default values
        defaultScale = transform.localScale.x;
        defaultGravity = rb2d.gravityScale;

        EventManager.AddListener("PlayerDie", OnPlayerDie);
        EventManager.AddListener("CollectPowerup", OnCollectPowerup);
    }

    void OnDestroy() {
        EventManager.RemoveListener("PlayerDie", OnPlayerDie);
        EventManager.RemoveListener("CollectPowerup", OnCollectPowerup);
    }

    void OnPlayerDie() {
        StopWaitingCoroutine();
    }

    void OnCollectPowerup(int powerupType) {
        soundsManager.Powerup();

        ResetAllPowerups();
        StopWaitingCoroutine();

        switch ((Powerup.PowerupType)powerupType) {
            case Powerup.PowerupType.MINIMIZE:
                MinimizePowerup();
                break;
            case Powerup.PowerupType.GRAVITY:
                GravityPowerup();
                break;
            case Powerup.PowerupType.BOUNCE:
                BouncePowerup();
                break;
            case Powerup.PowerupType.FREEZE:
                FreezePowerup();
                break;
            case Powerup.PowerupType.IMMORTALITY:
                ImmortalityPowerup();
                break;
        }

        PlayerStats.Bonus += 50;

        powerupCoroutine = StartCoroutine(WaitAndResetPowerups());
    }

    void MinimizePowerup() {
        animator.SetBool("IsMinimized", true);
    }

    void BouncePowerup() {
        player.IsBouncing = true;
    }

    void GravityPowerup() {
        rb2d.gravityScale = powerupGravity;
    }

    void FreezePowerup() {
        PlayerStats.IsFreezed = true;
    }

    void ImmortalityPowerup() {
        PlayerStats.IsImmortality = true;
    }

    void ResetMinimizePowerup() {
        animator.SetBool("IsMinimized", false);
    }

    void ResetBouncePowerup() {
        player.IsBouncing = false;
    }

    void ResetGravityPowerup() {
        rb2d.gravityScale = defaultGravity;
    }

    void ResetFreezePowerup() {
        PlayerStats.IsFreezed = false;
    }

    void ResetImmortalityPowerup() {
        PlayerStats.IsImmortality = false;
    }

    void ResetAllPowerups() {
        ResetMinimizePowerup();
        ResetBouncePowerup();
        ResetGravityPowerup();
        ResetFreezePowerup();
        ResetImmortalityPowerup();

        EventManager.TriggerEvent("ResetPowerups");
    }

    void StopWaitingCoroutine() {
        if (powerupCoroutine != null) {
            StopCoroutine(powerupCoroutine);
        }
    }

    IEnumerator WaitAndResetPowerups() {
        yield return new WaitForSeconds(powerupDuration);

        // powerup text flash
        EventManager.TriggerEvent("BeforeResetPowerups");

        yield return new WaitForSeconds(0.5f);

        ResetAllPowerups();
    }
}
