using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPanelController : MonoBehaviour
{
    [SerializeField] Text topScore;
    [SerializeField] Text yourScore;

    void Start() {
        topScore.text = Settings.TopScore.ToString();
        yourScore.text = PlayerStats.Score.ToString();
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            SceneManager.LoadScene("Game");
        }
    }
}
