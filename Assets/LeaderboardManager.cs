using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;

public class LeaderboardManager : MonoBehaviour
{
    public string leaderboardKey;
    public static LeaderboardManager instance;
    private LeaderboardController leaderboardController;

    void Awake()
    {
        if(instance == null)
        {
            DontDestroyOnLoad(this);
            instance = this;

        }
        leaderboardController = FindObjectOfType<LeaderboardController>();
        Debug.Log("Leaderboard Manager Started");
        StartCoroutine(StartSession());
    }

    public IEnumerator StartSession()
    {
        bool done = false;
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (response.success)
            {
                Debug.Log("Session Started");
                done = true;
            }
            else
            {
                Debug.Log("Session Failed");
                done = false;
            }
        });
        yield return new WaitWhile(() => !done);
    }

    [ContextMenu("Submit Score")]
    public IEnumerator SubmitScore(string name, int score)
    {   
        bool done = false;
        LootLockerSDKManager.SubmitScore(name, score, leaderboardKey, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Score Submitted");
                done = true;
                
            }
            else
            {
                Debug.Log("Score Failed");
                done = false;
            }
        });
        yield return new WaitWhile(() => !done);
    }
    
    [ContextMenu("Show Scores")]
    public IEnumerator ShowScores()
    {
        // for (int i =0 ; i < leaderboardLabels.Length; i++){
        //     Debug.Log(leaderboardLabels[i].text);
        // }
        bool done = false;
        leaderboardController = FindObjectOfType<LeaderboardController>();
        LootLockerSDKManager.GetScoreList(leaderboardKey, 5, 0, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Score List Received");
                LootLockerLeaderboardMember[] members = response.items;
                for (int i = 0; i < members.Length; i++)
                {
                    leaderboardController.leaderboardLabels[i].text = members[i].member_id;
                    leaderboardController.leaderboardScores[i].text = "Round " + members[i].score.ToString();
                }
                done = true;
            }
            else
            {
                Debug.Log("Score List Failed");
                done = false;
            }
        });

        yield return new WaitWhile(() => !done);
    }
    
}
