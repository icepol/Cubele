using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour {

    [SerializeField] AudioClip jump;
    [SerializeField] AudioClip pickupCoin;
    [SerializeField] AudioClip newCoin;
    [SerializeField] AudioClip explosion;
    [SerializeField] AudioClip newTopScore;
    [SerializeField] AudioClip powerup;
    [SerializeField] AudioClip wallCollision;

    // UI
    [SerializeField] AudioClip buttonClick;

    static SoundsManager instance;

	public SoundsManager Instance {
        get {
            if (instance == null) {
                DontDestroyOnLoad(gameObject);

                instance = GetComponent<SoundsManager>();
            }

            return instance;
        }
    }

    public void Play(AudioClip clip, Vector2? position = null) {
        if (Settings.Sounds) {
            AudioSource.PlayClipAtPoint(clip, position ?? Camera.main.transform.position);
        }
    }

    public void Jump(Vector2? position = null) {
        Play(jump, position);
    }

    public void PickupCoin(Vector2? position = null) {
        Play(pickupCoin, position);
    }

    public void NewCoin(Vector2? position = null) {
        Play(newCoin, position);
    }

    public void Explosion(Vector2? position = null) {
        Play(explosion, position);
    }

    public void Powerup(Vector2? position = null) {
        Play(powerup, position);
    }

    public void WallCollision(Vector2? position = null) {
        Play(wallCollision, position);
    }

    public void NewTopScore(Vector2? position = null) {
        Play(newTopScore, position);
    }

    // UI
    public void ButtonClick(Vector2? position = null) {
        Play(buttonClick, position);
    }
}
