using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarsSpawner : MonoBehaviour
{
    [SerializeField] GameObject starPrefab;

    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn() {
        while (true) {
            yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));

            GameObject star = Instantiate(
                starPrefab,
                new Vector2(Random.Range(-3f, 3f), -6),
                Quaternion.Euler(0, 0, Random.Range(0, 360)),
                transform
            );
        }
    }
}
