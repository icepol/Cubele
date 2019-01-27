﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    [SerializeField] GameObject explosionBonus;
    [SerializeField] GameObject explosionMine;

    Animator animator;
    SoundsManager soundsManager;

    bool isBonus = true;

    void Awake() {
        animator = GetComponent<Animator>();

        soundsManager = FindObjectOfType<SoundsManager>();
    }

    void Start()
    {
        animator.SetBool("IsBonus", isBonus);

        StartCoroutine(Animate());
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (!LevelManager.IsGameRunning)
            return;

        if (collision.gameObject.CompareTag("Player")) {
            if (isBonus) {
                soundsManager.PickupCoin();
                Explosion(explosionBonus);
                PlayerStats.Bonus += 50;
                EventManager.TriggerEvent("CollectBonus");
            }
            else {
                Explosion(explosionMine);
                EventManager.TriggerEvent("Collision");
            }

            Destroy(gameObject);
        }
    }

    IEnumerator Animate() {
        yield return new WaitForSeconds(Random.Range(0, 0.5f));

        while (true) {
            yield return new WaitForSeconds(1.6f);
            isBonus = false;
            animator.SetBool("IsBonus", isBonus);

            yield return new WaitForSeconds(0.2f);
            isBonus = true;
            animator.SetBool("IsBonus", isBonus);
        }
    }

    void Explosion(GameObject explosionPrefab) {
        GameObject explosionObject = Instantiate(explosionPrefab);
        explosionObject.transform.position = new Vector3(
            transform.position.x, transform.position.y, 0
        );
    }
}
