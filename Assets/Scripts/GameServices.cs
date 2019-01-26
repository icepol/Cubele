using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;

public struct UserScore {
    public string userName;
    public float value;

    public UserScore(float value, string userName) {
        this.value = value;
        this.userName = userName;
    }
}

public class GameServices : MonoBehaviour {

    static bool isInitialized;
    static bool showAuthentication = true;
    static bool isAuthenticated = false;

    static Dictionary<string, int> lastUnlockedAchievement = new Dictionary<string, int>();
    static Dictionary<string, UserScore[]> boardsScores = new Dictionary<string, UserScore[]>();

    public static void Initialize() {
        if (isInitialized)
            return;

#if UNITY_ANDROID
            PlayGamesPlatform.Activate();
            PlayGamesPlatform.DebugLogEnabled = true;
#endif

        if (showAuthentication) {
            showAuthentication = false;
            Social.localUser.Authenticate(OnUserAuthenticated);
        }

        isInitialized = true;
    }

    public static void ShowLeaderBoard() {
        if (!isAuthenticated) {
            Social.localUser.Authenticate(OnUserAuthenticated);
        }

#if UNITY_IPHONE
        Social.ShowLeaderboardUI();
#elif UNITY_ANDROID
            PlayGamesPlatform.Instance.ShowLeaderboardUI(Constants.TopScoreLeaderBoardId);
#else
#endif
    }

    public static void ReportScore(string boardId, int score) {
        if (Application.isEditor) {
            Debug.Log("GameServices: Report score " + score + " to board " + boardId);
            return;
        }

        if (isAuthenticated) {
            Social.ReportScore(
                score, boardId, OnReportScore
            );
        }
    }

    //public static void UnlockScoreAchievement(string achievementType, int value) {
    //    // iterate over given type achievements
    //    foreach (KeyValuePair<int, string> achievement in Constants.Achievements[achievementType]) {
    //        // last unlocked achievement in this type
    //        int lastUnlockingValue = lastUnlockedAchievement.ContainsKey(achievementType)
    //            ? lastUnlockedAchievement[achievementType]
    //            : 0;

    //        // if this is new value that shuld be unlocked
    //        if (achievement.Key <= value && lastUnlockingValue < achievement.Key) {
    //            // we can unlock next achievement
    //            UnlockAchievement(achievement.Value);

    //            // store unlocked achievement so we will not unlock it again
    //            lastUnlockedAchievement[achievementType] = achievement.Key;

    //            break;
    //        }
    //    }
    //}

    public static void UnlockAchievement(string achievementId) {
        if (Application.isEditor) {
            Debug.Log("GameServices: UnlockAchievement " + achievementId);
            return;
        }

        Social.ReportProgress(achievementId, 100f, (bool success) => {
            Debug.Log("GameServices: UnlockAchievement " + achievementId + ", sucess: " + success);
        });
    }

    static void OnUserAuthenticated(bool obj) {
        isAuthenticated = true;
    }

    static void OnReportScore(bool obj) {
    }

    public static void LoadScores(string boardId) {
        if (Application.isEditor) {
            Debug.Log("GameServices: Load scores from board " + boardId);
            EventManager.TriggerEvent("ScoresLoaded", boardId);
            return;
        }

        if (isAuthenticated) {
            Social.LoadScores(boardId, scores => {
                if (scores.Length > 0) {
                    // used for load users call
                    string[] playerIDs = new string[scores.Length];

                    for (int i = 0; i < scores.Length; i++) {
                        IScore score = scores[i];
                        Debug.Log("GameServices: Score " + score.value + " for player " + score.userID);

                        playerIDs[i] = score.userID;
                    }

                    // load users
                    Social.LoadUsers(playerIDs, userProfiles => {
                        // check if we loaded same number of elements
                        if (userProfiles.Length != playerIDs.Length) {
                            Debug.LogError("Different size of loaded scores and players");
                            return;
                        }

                        // prepare user scores list
                        UserScore[] usersScores = new UserScore[userProfiles.Length];

                        for (int i = 0; i < userProfiles.Length; i++) {
                            usersScores[i].value = scores[i].value;
                            usersScores[i].userName = userProfiles[i].userName;
                        }

                        // store data static variable for later use
                        boardsScores[boardId] = usersScores;

                        EventManager.TriggerEvent("ScoresLoaded", boardId);
                    });
                }
                else {
                    Debug.Log("GameServices: No scores in board " + boardId);
                }
            });
        }
    }

    public static UserScore[] GetScores(string boardId) {
        UserScore[] userScore;

        if (Application.isEditor) {
            userScore = new UserScore[] {
                new UserScore(2, "Jozef"),
                new UserScore(3, "Milan"),
                new UserScore(4, "Peter"),
                new UserScore(6, "Juraj"),
                new UserScore(8, "Juraj Xavier"),
                new UserScore(10, "Belohruz"),
                new UserScore(12, "Samir Oman"),
                new UserScore(16, "Foo Bar baz"),
                new UserScore(18, "Foo Bar baz 18"),
                new UserScore(18.5f, "Foo Bar baz 18.5"),
                new UserScore(18.8f, "Foo Bar baz 18.8"),
                new UserScore(19f, "Foo Bar baz 19"),
                new UserScore(19.5f, "Foo Bar baz 19.5"),
                new UserScore(22, "Fiktus Pictus 22"),
            };

            return userScore;
        }

        return boardsScores.ContainsKey(boardId) ? boardsScores[boardId] : null;
    }
}
