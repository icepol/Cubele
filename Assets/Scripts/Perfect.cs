using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perfect : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float destroyDelay = 1f;
    
    void Start()
    {
        Destroy(gameObject, destroyDelay);
    }

    void Update()
    {
        transform.Translate(new Vector2(0, moveSpeed * Time.deltaTime));
    }
}
