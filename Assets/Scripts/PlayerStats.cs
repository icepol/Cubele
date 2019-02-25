using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    public static int Score { get; set; }
    public static int Bonus { get; set; }
    public static float Distance { get; set; }
    public static float PlayTime { get; set; }
    public static bool IsNewTop { get; set; }
    public static int GamePlayCount { get; set; }
    public static bool ShowGameOverPanel { get; set; }

    public static void ResetAll() {
        Score = 0;
        Distance = 0;
        Bonus = 0;
        PlayTime = 0;
        IsNewTop = false;
        ShowGameOverPanel = false;
    }
}
