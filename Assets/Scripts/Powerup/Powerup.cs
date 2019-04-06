using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public enum PowerupType {
        MINIMIZE,
        GRAVITY,
        BOUNCE,
        FREEZE,
        IMMORTALITY,
    }

    [SerializeField] GameObject powerupText;
    [SerializeField] float speed = 1f;
    [SerializeField] PowerupType powerupType;

    Dictionary<int, string> textByType = new Dictionary<int, string> {
        {(int)PowerupType.MINIMIZE, "MINIMIZE"},
        {(int)PowerupType.GRAVITY, "GRAVITY"},
        {(int)PowerupType.BOUNCE, "BOUNCE"},
        {(int)PowerupType.FREEZE, "FREEZE"},
        {(int)PowerupType.IMMORTALITY, "IMMORTALITY"},
    };

    void Update()
    {
        if (!LevelManager.IsGameRunning)
            return;

        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (transform.position.y < -6f)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision) {
        Player player = collision.GetComponent<Player>();

        if (player == null)
            return;

        EventManager.TriggerEvent("CollectPowerup", (int)powerupType);

        SpawnPowerupText();

        Destroy(gameObject);
    }

    void SpawnPowerupText() {
        GameObject powerupTextObject = Instantiate(powerupText);

        powerupTextObject.transform.position = new Vector3(
            transform.position.x, transform.position.y, 0
        );

        powerupTextObject.GetComponent<PowerupText>().SetText(
            textByType[(int)powerupType]
        );
    }
}
