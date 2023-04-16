using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControls : MonoBehaviour
{
    public int roundNum = 1;
    public int Money;
    public int Lives;

    private SaveManager saveManager;
    
    void Awake()
    {
        saveManager = FindObjectOfType<SaveManager>();
        roundNum = saveManager.saveStats.roundNum;
        Money = saveManager.saveStats.creditNum;
        Lives = saveManager.saveStats.livesNum;
    }

    // Update is called once per frame
    void Update()
    {
        roundNum = GetComponent<EnemySpawn>().round;
        //Debug.Log(roundNum);
    }

}
