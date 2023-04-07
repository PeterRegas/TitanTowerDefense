using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    UIDocument mainMenuDoccument;
    public Button startButton, selectButton, quitButton, loadButton;
    
    public bool isLoad = false;
    

    // Start is called before the first frame update
    void Awake()
    {
        
        mainMenuDoccument = GetComponent<UIDocument>();
        var root = mainMenuDoccument.rootVisualElement;

        startButton = root.Q<Button>("StartButton");
        selectButton = root.Q<Button>("SelectLevelButton");
        quitButton = root.Q<Button>("QuitButton");
        loadButton = root.Q<Button>("LoadButton");
        startButton.RegisterCallback<ClickEvent>(StartButtonPressed);
        loadButton.RegisterCallback<ClickEvent>(loadButtonPressed);
        selectButton.RegisterCallback<ClickEvent>(selectButtonPressed);
        quitButton.RegisterCallback<ClickEvent>(quitButtonPressed);
    }


    public void StartButtonPressed(ClickEvent click)
    {
       // SaveState newSave = new SaveState();
       // Debug.Log(newSave.health);
       // saveManager.playerStats = newSave;
        Debug.Log("Start Pressed");
        SceneManager.LoadScene("SampleScene");
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
        //saveManager.loadPlayerStats();
        //SceneManager.LoadScene("TophatSurvivor");
        
    }
}
