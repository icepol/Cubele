using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Combo : MonoBehaviour
{
    [SerializeField] GameObject comboPanel;
    [SerializeField] Text comboMultiplierText;

    List<Bonus> bonuses;
    Bonus nextExpectedBonus;
    bool addNextToExpected;
    Coroutine resetCombo;

    void Start() {
        bonuses = new List<Bonus>();

        EventManager.AddListener("BonusCollision", OnBonusCollision);
    }

    void OnTriggerExit2D(Collider2D collision) {
        Bonus bonus = collision.GetComponent<Bonus>();
        
        if (bonus != null) {
            bonuses.Add(bonus);

            if (addNextToExpected) {
                // this will be next expected one
                nextExpectedBonus = bonus;
                addNextToExpected = false;
            }
        }
    }

    void OnDestroy() {
        EventManager.RemoveListener("BonusCollision", OnBonusCollision);
    }

    void OnBonusCollision(GameObject bonusObject) {
        Bonus currentBonus = bonusObject.GetComponent<Bonus>();

        if (currentBonus == null)
            return;

        if (nextExpectedBonus != null) {
            // check if this bonus match the expected one
            if (currentBonus == nextExpectedBonus) {
                PlayerStats.ComboMultiplier++;
                EventManager.TriggerEvent("Combo");

                if (resetCombo != null)
                    StopCoroutine(resetCombo);

                resetCombo = StartCoroutine(ResetCombo());
            }
            else {
                PlayerStats.ComboMultiplier = 1;
                addNextToExpected = false;

                EventManager.TriggerEvent("ResetCombo");
            }
        }

        // find bonus in the all bonus list
        int index = bonuses.IndexOf(currentBonus);

        if (index + 1 < bonuses.Count)
            // set next expected bonus
            nextExpectedBonus = bonuses[index + 1];
        else {
            // next will be the first comming one
            addNextToExpected = true;
            nextExpectedBonus = null;
        }

        // trim bonus list
        bonuses = bonuses.GetRange(index, bonuses.Count - index);
    }

    IEnumerator ResetCombo() {
        yield return new WaitForSeconds(5);

        PlayerStats.ComboMultiplier = 1;
        EventManager.TriggerEvent("ResetCombo");
    }
}
