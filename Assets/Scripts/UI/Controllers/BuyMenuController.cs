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
    private VisualElement background, showToBuy, showToUpgrade;
    private LevelControls levelControls;

    private bool emptyInRange = false, towerInRange = false;
    public bool buyMenuOpen = false;
    [SerializeField] GameObject[] towers;
    private Transform towerSpawnTransform;
    private GameObject tempTowerToDelete, tempTowerToUpgrade;
    private Label tooBrokeLabel;
    void Start()
    {
        //Debug.Log(buyMenuOpen);
        levelControls  = FindObjectOfType<LevelControls>();
        var root = buyMenuDocument.rootVisualElement;
        background = root.Q<VisualElement>("Background");
        background.style.display = DisplayStyle.None;
        showToBuy = root.Q<VisualElement>("ShowToBuy");
        showToBuy.style.display = DisplayStyle.None;
        showToUpgrade = root.Q<VisualElement>("ShowToUpgrade");
        showToUpgrade.style.display = DisplayStyle.None;

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
        if(Input.GetKeyDown(KeyCode.F) && emptyInRange){
            background.style.display = DisplayStyle.Flex;
            showToBuy.style.display = DisplayStyle.None;
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f;
            buyMenuOpen = true;
        }
        //If the player presses the escape key and is in range of the tower, the menu will close
        if(Input.GetKeyDown(KeyCode.Escape) && emptyInRange){
            background.style.display = DisplayStyle.None;
            showToBuy.style.display = DisplayStyle.Flex;
            tooBrokeLabel.style.display = DisplayStyle.None;
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1f;
            buyMenuOpen = false;
        }
        //Will delete the old tower if there was a loaded tower on top of it 
        if(emptyInRange && towerInRange){
            Debug.Log("Deleting old tower");
            emptyInRange = false;
            towerSpawnTransform = null;
            showToBuy.style.display = DisplayStyle.None;
            tooBrokeLabel.style.display = DisplayStyle.None;
            Destroy(tempTowerToDelete);
        }
        if(Input.GetKeyDown(KeyCode.F) && towerInRange){
            //Check if Spin tower script component exists in tempTowerToUpgrade
            if(tempTowerToUpgrade.GetComponent<Tower>() != null){
                Debug.Log("Spin Tower");
                //If the player has enough money, the tower will be upgraded
                if(levelControls.Money >= 100){
                    levelControls.Money -= 100;
                    tempTowerToUpgrade.GetComponent<Tower>().Upgrade();
                }
                //If the player does not have enough money, the tooBrokeLabel will be displayed
                else{
                    tooBrokeLabel.style.display = DisplayStyle.Flex;
                }
            }
            if(tempTowerToUpgrade.GetComponent<SpinTower>() != null){
                Debug.Log("Spin Tower");
                //If the player has enough money, the tower will be upgraded
                if(levelControls.Money >= 100){
                    levelControls.Money -= 100;
                    tempTowerToUpgrade.GetComponent<SpinTower>().Upgrade();
                }
                //If the player does not have enough money, the tooBrokeLabel will be displayed
                else{
                    tooBrokeLabel.style.display = DisplayStyle.Flex;
                }
            }
        }
    
    }

    //When the player enters the trigger zone, the inRange bool is set to true
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "emptyTower"){
            //Debug.Log("In Range");
            emptyInRange = true;
            tempTowerToDelete = other.gameObject;
            towerSpawnTransform = other.transform;
            showToBuy.style.display = DisplayStyle.Flex;
            tooBrokeLabel.style.display = DisplayStyle.None;
        }
        
        if(other.tag == "Tower"){
            towerInRange = true;
            tempTowerToUpgrade = other.gameObject;
            //Checks if you can still upgrade the tower
            if (tempTowerToUpgrade.GetComponent<Tower>() != null && tempTowerToUpgrade.GetComponent<Tower>().towerLevel < 5){
                showToUpgrade.style.display = DisplayStyle.Flex;
                tooBrokeLabel.style.display = DisplayStyle.None;
            }
            else if (tempTowerToUpgrade.GetComponent<SpinTower>() != null && tempTowerToUpgrade.GetComponent<SpinTower>().towerLevel < 5){
                showToUpgrade.style.display = DisplayStyle.Flex;
                tooBrokeLabel.style.display = DisplayStyle.None;
            }
            
        }
    }
    //When the player exits the trigger zone, the inRange bool is set to false
    private void OnTriggerExit(Collider other) {
        if(other.tag == "emptyTower"){
            emptyInRange = false;
            towerSpawnTransform = null;
            showToBuy.style.display = DisplayStyle.None;
            tooBrokeLabel.style.display = DisplayStyle.None;
        } 
        if(other.tag == "Tower"){
            towerInRange = false;
            showToUpgrade.style.display = DisplayStyle.None;
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
        buyMenuOpen = false;
        emptyInRange = false;
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
        buyMenuOpen = false;
    }
    

}
