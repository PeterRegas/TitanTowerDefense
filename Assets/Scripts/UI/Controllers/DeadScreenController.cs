using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using LootLocker.Requests;

public class DeadScreenController : MonoBehaviour
{
    [SerializeField] private UIDocument deadScreenDocument;
    private Button quitButton;
    private VisualElement background;
    private LevelControls levelControls;
    public string  leaderboardKey;
    private SaveManager saveManager;

    // Start is called before the first frame update
    void Start()
    {
        saveManager = FindObjectOfType<SaveManager>();
        levelControls  = FindObjectOfType<LevelControls>();
        var root = deadScreenDocument.rootVisualElement;
        quitButton = root.Q<Button>("QuitButton");
        background = root.Q<VisualElement>("Background");
        quitButton.RegisterCallback<ClickEvent>(quitButtonPressed);
        background.style.display = DisplayStyle.None;

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

    // Update is called once per frame
    void Update()
    {
        if (levelControls.Lives <= 0)
        {
            SubmitScore(levelControls.roundNum);
            background.style.display = DisplayStyle.Flex;
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f;
            
        }
    }
    void quitButtonPressed(ClickEvent click)
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
        
    }

    [ContextMenu("Submit Score")]
    public void SubmitScore(int score)
    {
        LootLockerSDKManager.SubmitScore(saveManager.playerName, score, leaderboardKey ,(response) =>
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
}
