using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    static int score;
    static int bonus;
    static float distance;

    public static int Score {
        get {
            return score;
        }
        set {
            score = value;
        }
    }

    public static int Bonus {
        get {
            return bonus;
        }
        set {
            bonus = value;
        }
    }

    public static float Distance {
        get {
            return distance;
        }
        set {
            if (value > distance) {
                distance = value;
            }
        }
    }

    public static void ResetAll() {
        score = 0;
        distance = 0;
        bonus = 0;
    }
}
