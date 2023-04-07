using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    private int i = 0;
    [SerializeField] int spawnRate;
    public GameObject enemy;
    [SerializeField] private Transform spawn;

    void FixedUpdate()
    {
        i++;
        //spawn enemy
        if(i>=spawnRate){
            //create the enemy      
            Instantiate(enemy, new Vector3(spawn.position.x, spawn.position.y, spawn.position.z), spawn.rotation);
            i=0;
        }
    }

}

