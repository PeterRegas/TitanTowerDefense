using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class BuyMenuController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] UIDocument buyMenuDocument;
    public Button buy1Button;
    private VisualElement background, showToBuy;

    private bool inRange = false;
    public bool menuOpen = false;
    [SerializeField] GameObject tower;
    private Transform towerSpawnTransform;
    void Start()
    {
        Debug.Log(menuOpen);
        var root = buyMenuDocument.rootVisualElement;
        background = root.Q<VisualElement>("Background");
        background.style.display = DisplayStyle.None;
        showToBuy = root.Q<VisualElement>("ShowToBuy");
        showToBuy.style.display = DisplayStyle.None;

        buy1Button = root.Q<Button>("Tower1Button");
        buy1Button.RegisterCallback<ClickEvent>(buy1ButtonPressed);

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
        }
    }
    //When the player exits the trigger zone, the inRange bool is set to false
    private void OnTriggerExit(Collider other) {
        if(other.tag == "emptyTower"){
            inRange = false;
            towerSpawnTransform = null;
            showToBuy.style.display = DisplayStyle.None;
        } 
    }

    private void buy1ButtonPressed(ClickEvent click)
    {
        
        Debug.Log("Tower 1 Button Pressed");
        Debug.Log("Tower Spawned");
        Debug.Log(towerSpawnTransform.position);
        makeTower(tower);
        

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

    
}
