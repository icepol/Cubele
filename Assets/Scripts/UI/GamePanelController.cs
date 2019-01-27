using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePanelController : MonoBehaviour
{
    [SerializeField] GameObject perfectLabel;

    Coroutine collectBonusCoroutine;

    void Start() {
        // disable "Perfect" label
        perfectLabel.SetActive(false);

        // remove game panel off the screen
        transform.localPosition = new Vector2(-1000, -1000);

        EventManager.AddListener("StartGame", OnStartGame);
        EventManager.AddListener("CollectBonus", OnCollectBonus);
    }

    void OnDestroy() {
        EventManager.RemoveListener("StartGame", OnStartGame);
        EventManager.RemoveListener("CollectBonus", OnCollectBonus);
    }

    void OnStartGame() {
        transform.localPosition = Vector2.zero;
    }

    void OnCollectBonus() {
        if (collectBonusCoroutine != null)
            StopCoroutine(collectBonusCoroutine);

        collectBonusCoroutine = StartCoroutine(CollectBonus());
    }

    IEnumerator CollectBonus() {
        perfectLabel.SetActive(true);
        yield return new WaitForSeconds(1.5f);

        perfectLabel.SetActive(false);
    }
}
