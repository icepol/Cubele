using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    static int score;
    static int bonus;
    static float distance;
    static float playTime;

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

    public static float PlayTime {
        get {
            return playTime;
        }
        set {
            playTime = value;
        }
    }

    public static void ResetAll() {
        score = 0;
        distance = 0;
        bonus = 0;
        playTime = 0;
    }
}
