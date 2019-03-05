using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboPanel : MonoBehaviour
{
    [SerializeField] Text comboMultiplierText;

    Coroutine multiplierOnScreen;

    void Start() {
        // hide panel from screen
        transform.localPosition = new Vector2(-1000, -1000);

        EventManager.AddListener("Combo", OnCombo);
    }

    void OnDestroy() {
        EventManager.RemoveListener("Combo", OnCombo);
    }

    void OnCombo() {
        if (PlayerStats.ComboMultiplier < 2)
            return;

        // update combo text
        comboMultiplierText.text = "x" + PlayerStats.ComboMultiplier;

        // add panel on screenm
        transform.localPosition = Vector2.zero;

        if (multiplierOnScreen != null)
            StopCoroutine(multiplierOnScreen);

        multiplierOnScreen = StartCoroutine(HideMultiplier());
    }

    IEnumerator HideMultiplier() {
        yield return new WaitForSeconds(0.8f);

        // hide panel from screen
        transform.localPosition = new Vector2(-1000, -1000);
    }
}
