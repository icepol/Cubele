using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float rotateSpeed = 1f;
    [SerializeField] float jumpSpeed = 10f;

    [SerializeField] GameObject explosion;
    [SerializeField] GameObject dust;

    bool moveRight;
    bool isDead;

    Rigidbody2D rb2d;
    SpriteRenderer spriteRenderer;
    SoundsManager soundsManager;
    Animator animator;
    Animator cameraAnimator;

    void Awake() {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        cameraAnimator = Camera.main.GetComponent<Animator>();

        soundsManager = FindObjectOfType<SoundsManager>();
    }

    void Start() {
        EventManager.AddListener("StartGame", OnStartGame);
        EventManager.AddListener("Collision", OnCollision);
    }

    void Update() {
        if (!LevelManager.IsGameRunning)
            return;

        if (isDead)
            return;

        transform.RotateAround(
            transform.position,
            moveRight ? Vector3.back : Vector3.forward,
            rotateSpeed * Time.deltaTime
        );

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            Jump();
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Wall"))
            EventManager.TriggerEvent("Collision");
    }

    void OnDestroy() {
        EventManager.RemoveListener("StartGame", OnStartGame);
        EventManager.RemoveListener("Collision", OnCollision);
    }

    void OnStartGame() {
        animator.SetBool("IsGameRunning", LevelManager.IsGameRunning);

        rb2d.bodyType = RigidbodyType2D.Dynamic;
        rb2d.velocity = Vector2.zero;

        rb2d.AddForce(new Vector2(
            0,
            jumpSpeed
        ));

        GameObject dustObject = Instantiate(dust);
        dustObject.transform.position = new Vector3(
            transform.position.x, transform.position.y - 0.15f, 0
        );

        soundsManager.Jump();
    }

    void Jump() {
        // change the direction
        moveRight = !moveRight;

        rb2d.velocity = Vector2.zero;

        rb2d.AddForce(new Vector2(
            moveRight ? moveSpeed : -moveSpeed,
            jumpSpeed
        ));

        GameObject dustObject = Instantiate(dust);
        dustObject.transform.position = new Vector3(
            transform.position.x, transform.position.y - 0.15f, 0
        );

        soundsManager.Jump();
    }

    void OnCollision() {
        if (isDead)
            return;

        isDead = true;

        cameraAnimator.SetTrigger("Shake");

        spriteRenderer.enabled = false;

        GameObject explosionObject = Instantiate(explosion);
        explosionObject.transform.position = new Vector3(
            transform.position.x, transform.position.y, 0
        );

        PlayerStats.Distance += transform.position.y;

        EventManager.TriggerEvent("PlayerDie");

        soundsManager.Explosion();

        StartCoroutine(GameOver());
    }

    IEnumerator GameOver() {
        yield return new WaitForSeconds(1.5f);

        // panel will be shown
        PlayerStats.ShowGameOverPanel = true;

        SceneManager.LoadScene("Game");
    }
}
