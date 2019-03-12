using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour {
    float startPosition;
    float endPosition;
    float speed;

    Animator animator;

    void Start() {
        animator = GetComponent<Animator>();

        startPosition = Random.Range(3, 6);
        endPosition = Random.Range(-3, -6);
        speed = Random.Range(0.5f, 1.5f);

        Vector2 position = transform.position;
        position.y = startPosition;
        transform.position = position;

        transform.localScale *= speed;
    }

    // Update is called once per frame
    void Update() {
        Vector2 position = transform.position;
        position.y -= (speed + (LevelManager.IsGameRunning ? 2f : 0)) * Time.deltaTime;
        transform.position = position;

        if (position.y < -6)
            Destroy(gameObject);
        else if (position.y < endPosition)
            animator.SetTrigger("Hide");
        else if (position.y < startPosition)
            animator.SetTrigger("Show");
    }
}
