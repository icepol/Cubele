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
    public static int ComboMultiplier { get; set; }
    public static int BonusCount { get; set; }
    public static bool IsFreezed { get; set; }
    public static bool IsImmortality { get; set; }

    public static void ResetAll() {
        Score = 0;
        Distance = 0;
        Bonus = 0;
        PlayTime = 0;
        IsNewTop = false;
        ShowGameOverPanel = false;
        ComboMultiplier = 1;
        BonusCount = 0;
        IsFreezed = false;
        IsImmortality = false;
    }
}
