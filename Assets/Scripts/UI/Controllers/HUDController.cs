using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HUDController : MonoBehaviour
{
    [SerializeField] private UIDocument hudDocument;
    private Label roundLabel;
    private Label moneyLabel;
    private Label healthLabel;

    private LevelControls levelControls;

    // Start is called before the first frame update
    void Start()
    {
        levelControls  = FindObjectOfType<LevelControls>();
        var root = hudDocument.rootVisualElement;
        roundLabel = root.Q<Label>("RoundLabel");
        moneyLabel = root.Q<Label>("CreditsLabel");
        healthLabel = root.Q<Label>("HealthLabel");
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("HUDController");
        // Debug.Log(saveManager.saveStats.roundNum);
        // Debug.Log(levelControls.Money);
        // Debug.Log(saveManager.saveStats.livesNum);
        
        roundLabel.text = "Round: " + levelControls.roundNum;
        moneyLabel.text = "Credits: " + levelControls.Money;
        healthLabel.text = "Health: " + levelControls.Lives;
    }
}
