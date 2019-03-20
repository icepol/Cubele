using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour {

    const string SOUNDS_KEY = "sounds";
    const string VIBRATIONS_KEY = "vibrations";
    const string TOP_SCORE_KEY = "top_score";
    const string LAST_SCORE_KEY = "last_score";
    const string TOP_DISTANCE_KEY = "top_distance";
    const string IS_NEW_TOP_KEY = "is_new_top";
    const string IS_FIRST_RUN = "is_first_run";
    const string SHOW_CONTINUE_BUTTIN = "show_continue_button";
    const string IS_REWARD_REQUESTED = "reward_requested";
    const string GAME_PLAY_COUNT = "game_play_count";
    const string RATE_FORM_REQUEST_COUNT = "rate_form_request_count";
    const string RATE_FORM_TOTAL_COUNT = "rate_form_total_count";
    const string RATE_FORM_USED = "rate_form_used";

    public static bool Sounds {
        get {
            return PlayerPrefs.GetInt(SOUNDS_KEY, 1) > 0;
        }

        set {
            PlayerPrefs.SetInt(SOUNDS_KEY, value ? 1 : 0);
        }
    }

    public static bool Vibrations {
        get {
            return PlayerPrefs.GetInt(VIBRATIONS_KEY, 1) > 0;
        }

        set {
            PlayerPrefs.SetInt(VIBRATIONS_KEY, value ? 1 : 0);
        }
    }

    public static int TopScore {
        get {
            return PlayerPrefs.GetInt(TOP_SCORE_KEY, 0);
        }

        set {
            PlayerPrefs.SetInt(TOP_SCORE_KEY, value);
        }
    }

    public static int LastScore {
        get {
            return PlayerPrefs.GetInt(LAST_SCORE_KEY, 0);
        }

        set {
            PlayerPrefs.SetInt(LAST_SCORE_KEY, value);

            if (value > TopScore) {
                TopScore = value;
                IsNewTop = true;
            }
            else {
                IsNewTop = false;
            }
        }
    }

    public static float TopDistance {
        get {
            return PlayerPrefs.GetFloat(TOP_DISTANCE_KEY, 0);
        }

        set {
            PlayerPrefs.SetFloat(TOP_DISTANCE_KEY, value);
        }
    }

    public static bool IsNewTop {
        get {
            return PlayerPrefs.GetInt(IS_NEW_TOP_KEY, 1) > 0;
        }

        set {
            PlayerPrefs.SetInt(IS_NEW_TOP_KEY, value ? 1 : 0);
        }
    }

    public static bool IsFirstRun {
        get {
            return PlayerPrefs.GetInt(IS_FIRST_RUN, 1) > 0;
        }

        set {
            PlayerPrefs.SetInt(IS_FIRST_RUN, value ? 1 : 0);
        }
    }

    public static bool IsRewardRequested {
        get {
            return PlayerPrefs.GetInt(IS_REWARD_REQUESTED, 1) > 0;
        }

        set {
            PlayerPrefs.SetInt(IS_REWARD_REQUESTED, value ? 1 : 0);
        }
    }

    public static int GamePlayCount {
        get {
            return PlayerPrefs.GetInt(GAME_PLAY_COUNT, 0);
        }

        set {
            PlayerPrefs.SetInt(GAME_PLAY_COUNT, value);
        }
    }

    public static int RateFormRequestCount {
        get {
            return PlayerPrefs.GetInt(RATE_FORM_REQUEST_COUNT, 0);
        }

        set {
            PlayerPrefs.SetInt(RATE_FORM_REQUEST_COUNT, value);
        }
    }

    public static int RateFormTotalCount {
        get {
            return PlayerPrefs.GetInt(RATE_FORM_TOTAL_COUNT, 0);
        }

        set {
            PlayerPrefs.SetInt(RATE_FORM_TOTAL_COUNT, value);
        }
    }

    public static bool RateFormUsed {
        get {
            return PlayerPrefs.GetInt(RATE_FORM_USED, 0) > 0;
        }

        set {
            PlayerPrefs.SetInt(RATE_FORM_USED, value ? 1 : 0);
        }
    }
}
