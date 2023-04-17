using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    UIDocument mainMenuDoccument;
    [SerializeField] private UIDocument leaderBoardDocument;
    public Button startButton, selectButton, quitButton, loadButton, leaderButton;
    private SaveManager saveManager;
    public bool isLoad = false;

    private VisualElement background, leaderBoard;
    

    // Start is called before the first frame update
    void Awake()
    {
        saveManager = FindObjectOfType<SaveManager>();
        mainMenuDoccument = GetComponent<UIDocument>();
        var root = mainMenuDoccument.rootVisualElement;
        var leaderRoot = leaderBoardDocument.rootVisualElement;

        //All stuff in the main menu
        startButton = root.Q<Button>("StartButton");
        selectButton = root.Q<Button>("SelectLevelButton");
        quitButton = root.Q<Button>("QuitButton");
        loadButton = root.Q<Button>("LoadButton");
        leaderButton = root.Q<Button>("LeaderButton");
        background = root.Q<VisualElement>("Background");
        startButton.RegisterCallback<ClickEvent>(StartButtonPressed);
        loadButton.RegisterCallback<ClickEvent>(loadButtonPressed);
        selectButton.RegisterCallback<ClickEvent>(selectButtonPressed);
        quitButton.RegisterCallback<ClickEvent>(quitButtonPressed);
        leaderButton.RegisterCallback<ClickEvent>(leaderButtonPressed);

        //All stuff in the leader board
        leaderBoard = leaderRoot.Q<VisualElement>("Background");
        leaderBoard.style.display = DisplayStyle.None;
    }


    public void StartButtonPressed(ClickEvent click)
    {
       // SaveState newSave = new SaveState();
       // Debug.Log(newSave.health);
       // saveManager.playerStats = newSave;
        Debug.Log("Start Pressed");
        saveManager.saveStats = new SaveState();
        SceneManager.LoadScene("Level 1");
    }
    void selectButtonPressed(ClickEvent click)
    {
        //SceneManager.LoadScene("InstructionsScene");
        selectButton.style.display = DisplayStyle.None;
    }
    void quitButtonPressed(ClickEvent click)
    {
        Debug.Log("quit");
        Application.Quit();
    }
    void loadButtonPressed(ClickEvent click)
    {
        saveManager.loadGameStats();
        if (saveManager.saveStats != null){
            SceneManager.LoadScene(saveManager.saveStats.levelName);
        }else{
            loadButton.text = "No Save Found";
        }
    }
    void leaderButtonPressed(ClickEvent click)
    {
        background.style.display = DisplayStyle.None;
        leaderBoard.style.display = DisplayStyle.Flex;
        
    }
}
