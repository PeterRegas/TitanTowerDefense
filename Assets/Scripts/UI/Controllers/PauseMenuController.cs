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

    [SerializeField] private BuyMenuController buyMenuController;

    private SaveManager saveManager;

    // Start is called before the first frame update
    void Awake()
    {
        saveManager = FindObjectOfType<SaveManager>();
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
        if (Input.GetKeyDown(KeyCode.Escape) && isPaused == false && !buyMenuController.menuOpen )
        {
            pause();
            Debug.Log("pause");
            
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused == true)
        {
            resume();
            Debug.Log("resume");
            
        }
    }
    public void pause()
    {
        saveButton.text = "Save";
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
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
        isPaused = false;
    }
    void saveButtonPressed(ClickEvent click)
    {
        saveManager.saveGameStats();
        saveButton.text = "Saved!";

    }
}
