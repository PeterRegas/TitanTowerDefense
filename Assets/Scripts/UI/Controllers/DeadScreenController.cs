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

    private LeaderboardManager leaderboardManager;

    // Start is called before the first frame update
    void Start()
    {
        leaderboardManager = FindObjectOfType<LeaderboardManager>();
        saveManager = FindObjectOfType<SaveManager>();
        levelControls  = FindObjectOfType<LevelControls>();
        var root = deadScreenDocument.rootVisualElement;
        quitButton = root.Q<Button>("QuitButton");
        background = root.Q<VisualElement>("Background");
        quitButton.RegisterCallback<ClickEvent>(quitButtonPressed);
        background.style.display = DisplayStyle.None;

    }

    // Update is called once per frame
    void Update()
    {
        if (levelControls.Lives <= 0)
        {
            background.style.display = DisplayStyle.Flex;
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f;
            
        }
    }
    void quitButtonPressed(ClickEvent click)
    {
        StartCoroutine(leaderboardManager.SubmitScore(saveManager.playerName, levelControls.roundNum));
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
        
    }

}
