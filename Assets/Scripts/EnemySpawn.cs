using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemySpawn : MonoBehaviour
{
    private int i,j,k = 0;
    [SerializeField] public int spawnRate;
    private int ModifiedSpawnRate;
    public GameObject[] enemy;
    [SerializeField] private Transform[] spawn;

    [SerializeField] public int round;
    [SerializeField] private int totalEnemies;
    public bool startRound;
    public GameObject levelcontrol;

    private HUDController hudController;

    public int totalDead;

    // Start is called before the first frame update
    void Start()
    {
        totalDead = 0;
        hudController = FindObjectOfType<HUDController>();
        levelcontrol = GameObject.FindGameObjectWithTag("levelcontrol");
        round = levelcontrol.GetComponent<LevelControls>().roundNum;
        Debug.Log("Round is: " + round);
    }

    void FixedUpdate()
    {
        i++;
        totalEnemies = round * 5;
        ModifiedSpawnRate = (spawnRate * 10)/round;
        //spawn enemy
        if(k<=totalEnemies){
            if(i>=ModifiedSpawnRate){
                //create the enemy      
                Instantiate(enemy[0], new Vector3(spawn[0].position.x, spawn[0].position.y, spawn[0].position.z), spawn[0].rotation);
                i=0;
                j++;
                k++;
                if(j>=5){
                    totalEnemies++;
                    Instantiate(enemy[1], new Vector3(spawn[1].position.x, spawn[1].position.y, spawn[1].position.z), spawn[1].rotation);
                    j=0;
                    k++;
                    Debug.Log("Spawned a boss");
                    Debug.Log("Total enemy: " + totalEnemies);
                    Debug.Log("Total k: " + k);
                    
                }
            }
        }
        else{
            if(startRound){
                round++;
                GetComponent<LevelControls>().Money+=50;
                k=0;
                hudController.bottomNextRound.style.display = DisplayStyle.None;
                totalDead = 0;
            }
        }
        if (totalDead==(totalEnemies + totalEnemies/5) || totalDead ==(totalEnemies + totalEnemies/5)){
            hudController.bottomNextRound.style.display = DisplayStyle.Flex;
        }
    } 
    void Update(){
        
        if(Input.GetKeyDown(KeyCode.E)){
            startRound = true;
        }
        if(Input.GetKeyUp(KeyCode.E)){
            startRound = false;
        }
        // if(Input.GetKeyDown(KeyCode.R)){
        //     Debug.Log("Total enemy: " + (totalEnemies + totalEnemies/5));
        //     Debug.Log("Total dead: " + totalDead);
        // }
    }

}

