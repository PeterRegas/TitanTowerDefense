using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    private int i,j,k = 0;
    [SerializeField] public int spawnRate;
    private int ModifiedSpawnRate;
    public GameObject[] enemy;
    [SerializeField] private Transform[] spawn;

    [SerializeField] public int round = 1;
    [SerializeField] private int totalEnemies;
    public bool startRound;

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
                    Instantiate(enemy[1], new Vector3(spawn[1].position.x, spawn[1].position.y, spawn[1].position.z), spawn[1].rotation);
                    j=0;
                    k++;
                }
            }
        }
        else{
            if(startRound){
                round++;
                k=0;
            }
        }
    } 
    void Update(){
        if(Input.GetKeyDown(KeyCode.E)){
            startRound = true;
        }
        if(Input.GetKeyUp(KeyCode.E)){
            startRound = false;
        }
    }

}

