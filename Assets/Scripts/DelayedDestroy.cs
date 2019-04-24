using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedDestroy : MonoBehaviour
{
    [SerializeField] float delay = 1f;

    void Start() {
        Destroy(gameObject, delay);
    }
}
