using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    [SerializeField] GameObject explosionBonus;
    [SerializeField] GameObject explosionMine;
    [SerializeField] GameObject perfect;

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
                PlayerBonusCollision();
            }
            else {
                Explosion(explosionMine);
                EventManager.TriggerEvent("Collision");
            }

            Destroy(gameObject);
        }
    }

    IEnumerator Animate() {
        yield return new WaitForSeconds(Random.Range(0, 1f));

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

    void PlayerBonusCollision() {
        soundsManager.PickupCoin();
        Explosion(explosionBonus);

        EventManager.TriggerEvent("BonusCollision", gameObject);

        PlayerStats.Bonus += 50 * PlayerStats.ComboMultiplier;

        GameObject perfectGameObject = Instantiate(perfect, transform.position, Quaternion.identity);
        perfectGameObject.GetComponent<Perfect>().SetComboMultiplier(PlayerStats.ComboMultiplier);
    }
}
