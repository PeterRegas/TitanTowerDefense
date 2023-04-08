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
    private VisualElement background;

    private bool inRange = false;
    [SerializeField] GameObject tower;
    private Transform towerSpawnTransform;
    void Start()
    {
        var root = buyMenuDocument.rootVisualElement;
        background = root.Q<VisualElement>("Background");
        background.style.display = DisplayStyle.None;

        buy1Button = root.Q<Button>("Tower1Button");
        buy1Button.RegisterCallback<ClickEvent>(buy1ButtonPressed);

    }

     private void Update() {
        if(Input.GetKeyDown(KeyCode.F) && inRange){
            background.style.display = DisplayStyle.Flex;
            UnityEngine.Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f;
        }
    
    }

    //When the player enters the trigger zone, the inRange bool is set to true
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "emptyTower"){
            //Debug.Log("In Range");
            inRange = true;
            towerSpawnTransform = other.transform;
        }
    }
    //When the player exits the trigger zone, the inRange bool is set to false
    private void OnTriggerExit(Collider other) {
        if(other.tag == "emptyTower"){
            inRange = false;
            towerSpawnTransform = null;
        } 
    }

    private void buy1ButtonPressed(ClickEvent click)
    {
        
        Debug.Log("Tower 1 Button Pressed");
        Debug.Log("Tower Spawned");
        Debug.Log(towerSpawnTransform.position);
        
        Instantiate(tower, towerSpawnTransform.position, towerSpawnTransform.rotation);
        Destroy(towerSpawnTransform.gameObject);
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        background.style.display = DisplayStyle.None;
        Time.timeScale = 1f;
        

    }

    
}
