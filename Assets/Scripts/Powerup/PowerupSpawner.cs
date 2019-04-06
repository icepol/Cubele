using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] powerups;

    Coroutine spawner;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.AddListener("StartGame", OnStartGame);
        EventManager.AddListener("PlayerDie", OnPlayerDie);
    }

    void OnStartGame() {
        spawner = StartCoroutine(SpawnNextPowerup());
    }

    void OnPlayerDie() {
        if (spawner != null)
            StopCoroutine(spawner);
    }

    IEnumerator SpawnNextPowerup() {
        yield return new WaitForSeconds(Random.Range(4f, 8f));

        while (true) {
            if (Random.Range(0, 100) > 50) {
                GameObject powerup = Instantiate(
                    powerups[Random.Range(0, powerups.Length)], 
                    new Vector2(Random.Range(-1f, 1f), transform.position.y), 
                    Quaternion.identity
                );

                // TODO: make a sound?

                yield return new WaitForSeconds(Random.Range(4f, 8f));
            }
            else {
                yield return new WaitForSeconds(Random.Range(1f, 2f));
            }
        }

    }
}
