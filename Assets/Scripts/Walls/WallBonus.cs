using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBonus : MonoBehaviour
{
    [SerializeField] Transform[] positions;
    [SerializeField] GameObject bonus;

    void Start() {
        foreach (Transform position in positions) {
            if (Random.Range(0, 100) > 70) {
                Instantiate(bonus, position.position, Quaternion.identity, transform);
            }
        }
    }
}
