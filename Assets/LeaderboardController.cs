using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using UnityEngine.UIElements;

public class LeaderboardController : MonoBehaviour 
{
    public int memberID, score;
    public string  leaderboardKey;

    UIDocument leaderboardDocument;
    [SerializeField] private UIDocument mainMenuDocument;
    private Button closeButton, leaderButton;
    private VisualElement background, mainMenu;
    private Label [] leaderboardLabels = new Label[5];
    private Label [] leaderboardScores = new Label[5];

    // Start is called before the first frame update
    void Start()
    {
        leaderboardDocument = GetComponent<UIDocument>();
        var root = leaderboardDocument.rootVisualElement;

        var mainMenuRoot = mainMenuDocument.rootVisualElement;
        mainMenu = mainMenuRoot.Q<VisualElement>("Background");
       // mainMenu.style.display = DisplayStyle.None;

        closeButton = root.Q<Button>("CloseButton");
        closeButton.RegisterCallback<ClickEvent>(closeButtonPressed);

        leaderButton = mainMenuRoot.Q<Button>("LeaderButton");
        leaderButton.RegisterCallback<ClickEvent>(leaderButtonPressed);

        background = root.Q<VisualElement>("Background");
        background.style.display = DisplayStyle.None;

        leaderboardLabels[0] = root.Q<Label>("Player1");
        leaderboardLabels[1] = root.Q<Label>("Player2");
        leaderboardLabels[2] = root.Q<Label>("Player3");
        leaderboardLabels[3] = root.Q<Label>("Player4");
        leaderboardLabels[4] = root.Q<Label>("Player5");

        leaderboardScores[0] = root.Q<Label>("Player1Score");
        leaderboardScores[1] = root.Q<Label>("Player2Score");
        leaderboardScores[2] = root.Q<Label>("Player3Score");
        leaderboardScores[3] = root.Q<Label>("Player4Score");
        leaderboardScores[4] = root.Q<Label>("Player5Score");

        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (response.success)
            {
                Debug.Log("Session Started");
                
            }
            else
            {
                Debug.Log("Session Failed");
            }
        });
    }

    [ContextMenu("Submit Score")]
    public void SubmitScore()
    {
        LootLockerSDKManager.SubmitScore("1234", 55, leaderboardKey ,(response) =>
        {
            if (response.success)
            {
                Debug.Log("Score Submitted");
            }
            else
            {
                Debug.Log("Score Failed");
            }
        });
    }
    [ContextMenu("Show Scores")]
    public void ShowScores (){

        // for (int i =0 ; i < leaderboardLabels.Length; i++){
        //     Debug.Log(leaderboardLabels[i].text);
        // }

        LootLockerSDKManager.GetScoreList(leaderboardKey, 5, 0, (response) =>
        {
            if (response.statusCode == 200)
            {
                Debug.Log("Score List Received");
                LootLockerLeaderboardMember [] members = response.items;
                for (int i = 0; i < members.Length; i++)
                {
                    leaderboardLabels[i].text = members[i].member_id;
                    leaderboardScores[i].text = "Round " + members[i].score.ToString();
                }
            }
            else
            {
                Debug.Log("Score List Failed");
            }
        });
    }

    private void closeButtonPressed(ClickEvent evt)
    {
        background.style.display = DisplayStyle.None;
        mainMenu.style.display = DisplayStyle.Flex;
    }
    private void leaderButtonPressed(ClickEvent click)
    {
        mainMenu.style.display = DisplayStyle.None;
        background.style.display = DisplayStyle.Flex;
        ShowScores();
    }
}
