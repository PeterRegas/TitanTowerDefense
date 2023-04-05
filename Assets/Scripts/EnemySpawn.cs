using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    int i = 0;
    public GameObject enemy;
    [SerializeField] private Transform spawn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        i++;
        //spawn enemy
        if(i>=200){
            //create the enemy      
            Instantiate(enemy, new Vector3(spawn.position.x, spawn.position.y, spawn.position.z), spawn.rotation);
            i=0;
        }
    }
    void OnTriggerEnter(Collider thing)
    {
        if(thing.tag == "Exit"){
            //thing.GetComponent<enemy>().Hit(damage);
            Destroy(gameObject);
        }
        
    }
}

