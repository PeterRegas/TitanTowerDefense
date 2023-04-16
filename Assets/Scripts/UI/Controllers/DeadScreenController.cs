using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class DeadScreenController : MonoBehaviour
{
    [SerializeField] private UIDocument deadScreenDocument;
    private Button quitButton;
    private VisualElement background;
    private LevelControls levelControls;

    // Start is called before the first frame update
    void Start()
    {
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
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
        
    }
}
