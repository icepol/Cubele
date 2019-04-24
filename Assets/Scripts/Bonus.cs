using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    [SerializeField] GameObject explosionBonus;
    [SerializeField] GameObject explosionMine;
    [SerializeField] GameObject perfect;

    [SerializeField] GameObject bonusEffect;
    [SerializeField] GameObject enemyEffect;

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
            else if (PlayerStats.IsImmortality) {
                // do nothing here
                return;
            }
            else {
                PlayerEnemyCollision();
            }

            Destroy(gameObject);
        }
    }

    IEnumerator Animate() {
        yield return new WaitForSeconds(Random.Range(0f, 1f));

        while (true) {
            yield return new WaitForSeconds(1.6f);

            // if bonus is freezed all the time it's bonus
            isBonus = PlayerStats.IsFreezed;
            animator.SetBool("IsBonus", isBonus);

            yield return new WaitForSeconds(0.2f);

            if (!isBonus)
                MakeEffect(bonusEffect);

            isBonus = true;
            animator.SetBool("IsBonus", isBonus);
        }
    }

    void Explosion(GameObject explosionPrefab) {
        GameObject explosionObject = Instantiate(explosionPrefab);
        explosionObject.transform.position = new Vector3(
            transform.position.x, transform.position.y, -1
        );
    }

    void PlayerBonusCollision() {
        soundsManager.PickupCoin();
        Explosion(explosionBonus);

        EventManager.TriggerEvent("BonusCollision", gameObject);

        PlayerStats.BonusCount += 1;
        PlayerStats.Bonus += 50 * PlayerStats.ComboMultiplier;

        GameObject perfectGameObject = Instantiate(perfect, transform.position, Quaternion.identity);
        perfectGameObject.GetComponent<Perfect>().SetComboMultiplier(PlayerStats.ComboMultiplier);

        MakeEffect(bonusEffect);
    }

    void PlayerEnemyCollision() {
        Explosion(explosionMine);
        EventManager.TriggerEvent("Collision");

        MakeEffect(enemyEffect);
    }

    void MakeEffect(GameObject prefab) {
        GameObject effect = Instantiate(prefab);
        effect.transform.position = transform.position;
        effect.GetComponent<PlayerJumpEffect>().SetRotation(transform.localRotation);
    }
}
