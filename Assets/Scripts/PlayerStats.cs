using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    static int score;
    static int bonus;
    static float distance;
    static float playTime;
    static bool isNewTop; 

    public static int Score { get; set; }
    public static int Bonus { get; set; }
    public static float Distance { get; set; }
    public static float PlayTime { get; set; }
    public static bool IsNewTop { get; set; }

    public static void ResetAll() {
        Score = 0;
        Distance = 0;
        Bonus = 0;
        PlayTime = 0;
        IsNewTop = false;
    }
}
