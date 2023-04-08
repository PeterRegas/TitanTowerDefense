using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{

    [SerializeField] UIDocument pauseMenuDocument;
    public Button resumeButton, saveButton, quitButton;
    private VisualElement background;
    private bool isPaused = false;

    

    // Start is called before the first frame update
    void Awake()
    {
        
        
        var root = pauseMenuDocument.rootVisualElement;

        resumeButton = root.Q<Button>("ResumeButton");
        saveButton = root.Q<Button>("SaveButton");
        quitButton = root.Q<Button>("QuitButton");
        background = root.Q<VisualElement>("Background");
        resumeButton.RegisterCallback<ClickEvent>(resumeButtonPressed);
        saveButton.RegisterCallback<ClickEvent>(saveButtonPressed);
        quitButton.RegisterCallback<ClickEvent>(quitButtonPressed);
        background.style.display = DisplayStyle.None;
    }

     private void Update()
    {
        if (Input.GetButtonDown("Cancel") && isPaused == false)
        {
            pause();
            Debug.Log("pause");
            
        }
        else if (Input.GetButtonDown("Cancel") && isPaused == true)
        {
            resume();
            Debug.Log("resume");
            
        }
    }
    public void pause()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        background.style.display = DisplayStyle.Flex;
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void resume()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        background.style.display = DisplayStyle.None;
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void resumeButtonPressed(ClickEvent click)
    {
       // SaveState newSave = new SaveState();
       // Debug.Log(newSave.health);
       // saveManager.playerStats = newSave;
        resume();
    }
    
    void quitButtonPressed(ClickEvent click)
    {
        Debug.Log("quit");
        Application.Quit();
    }
    void saveButtonPressed(ClickEvent click)
    {
        //saveManager.loadPlayerStats();
        //SceneManager.LoadScene("TophatSurvivor");
        
    }
}
