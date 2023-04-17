using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

//Place in player as component
public class BuyMenuController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] UIDocument buyMenuDocument;
    public Button buy1Button, buy2Button, closeButton;
    private VisualElement background, showToBuy;
    private LevelControls levelControls;

    private bool inRange = false;
    public bool menuOpen = false;
    [SerializeField] GameObject[] towers;
    private Transform towerSpawnTransform;

    private Label tooBrokeLabel;
    void Start()
    {
        Debug.Log(menuOpen);
        levelControls  = FindObjectOfType<LevelControls>();
        var root = buyMenuDocument.rootVisualElement;
        background = root.Q<VisualElement>("Background");
        background.style.display = DisplayStyle.None;
        showToBuy = root.Q<VisualElement>("ShowToBuy");
        showToBuy.style.display = DisplayStyle.None;

        buy1Button = root.Q<Button>("Tower1Button");
        buy1Button.RegisterCallback<ClickEvent>(buy1ButtonPressed);
        buy2Button = root.Q<Button>("Tower2Button");
        buy2Button.RegisterCallback<ClickEvent>(buy2ButtonPressed);
        tooBrokeLabel = root.Q<Label>("TooBrokeLevel");
        tooBrokeLabel.style.display = DisplayStyle.None;
        closeButton = root.Q<Button>("CloseButton");
        closeButton.RegisterCallback<ClickEvent>(closeButtonPressed);
    }

     private void Update() {
        //If the player presses the F key and is in range of the tower, the menu will open
        if(Input.GetKeyDown(KeyCode.F) && inRange){
            background.style.display = DisplayStyle.Flex;
            showToBuy.style.display = DisplayStyle.None;
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f;
            menuOpen = true;
        }
        //If the player presses the escape key and is in range of the tower, the menu will close
        if(Input.GetKeyDown(KeyCode.Escape) && inRange){
            background.style.display = DisplayStyle.None;
            showToBuy.style.display = DisplayStyle.Flex;
            tooBrokeLabel.style.display = DisplayStyle.None;
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1f;
            menuOpen = false;
        }
    
    }

    //When the player enters the trigger zone, the inRange bool is set to true
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "emptyTower"){
            //Debug.Log("In Range");
            inRange = true;
            towerSpawnTransform = other.transform;
            showToBuy.style.display = DisplayStyle.Flex;
            tooBrokeLabel.style.display = DisplayStyle.None;
        }
    }
    //When the player exits the trigger zone, the inRange bool is set to false
    private void OnTriggerExit(Collider other) {
        if(other.tag == "emptyTower"){
            inRange = false;
            towerSpawnTransform = null;
            showToBuy.style.display = DisplayStyle.None;
            tooBrokeLabel.style.display = DisplayStyle.None;
        } 
    }

    private void buy1ButtonPressed(ClickEvent click)
    {
        Debug.Log("Tower 1 Button Pressed");
        Debug.Log("Tower Spawned");
        Debug.Log(towerSpawnTransform.position);
        if(levelControls.Money >= 100){
            levelControls.Money -= 100;
            makeTower(towers[0]);
        }
        else{
            tooBrokeLabel.style.display = DisplayStyle.Flex;
        }

    }

    private void buy2ButtonPressed(ClickEvent click)
    {
        Debug.Log("Tower 2 Button Pressed");
        Debug.Log("Tower Spawned");
        Debug.Log(towerSpawnTransform.position);
        if(levelControls.Money >= 50){
            levelControls.Money -= 50;
            makeTower(towers[1]);
        }
        else{
            tooBrokeLabel.style.display = DisplayStyle.Flex;
        }
        
    }


    private void makeTower(GameObject tower){
        menuOpen = false;
        inRange = false;
        Instantiate(tower, towerSpawnTransform.position, towerSpawnTransform.rotation);
        Destroy(towerSpawnTransform.gameObject);
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        background.style.display = DisplayStyle.None;
        Time.timeScale = 1f;
    }

    private void closeButtonPressed(ClickEvent click)
    {
        //Debug.Log("Close Button Pressed");
        background.style.display = DisplayStyle.None;
        showToBuy.style.display = DisplayStyle.Flex;
        tooBrokeLabel.style.display = DisplayStyle.None;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        menuOpen = false;
    }
    
}
