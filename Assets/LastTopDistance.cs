using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastTopDistance : MonoBehaviour
{
    [SerializeField] GameObject perfectPrefab;

    SoundsManager soundsManager;

    bool isAfterCollision;

    void Awake() {
        soundsManager = FindObjectOfType<SoundsManager>();
    }

    void Start()
    {
        if (Settings.TopDistance > 0) {
            // set propper position
            transform.position = new Vector2(0, Settings.TopDistance);
            EventManager.AddListener("WallMove", OnWallMove);
        }
        else
            Destroy(gameObject);
    }

    void OnDestroy() {
        EventManager.RemoveListener("WallMove", OnWallMove);
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (!isAfterCollision && collision.gameObject.CompareTag("Player")) {
            soundsManager.PickupCoin();

            PlayerStats.Bonus += 100;

            Instantiate(perfectPrefab, transform.position, Quaternion.identity);

            isAfterCollision = true;
        }
    }

    void OnWallMove(Vector3 offset) {
        transform.Translate(offset);
    }
}
