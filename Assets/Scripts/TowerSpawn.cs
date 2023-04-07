using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawn : MonoBehaviour
{
    private bool inRange = false;
    [SerializeField] GameObject tower;
    private void Update() {
        if(Input.GetKeyDown(KeyCode.F) && inRange){
            Instantiate(tower, gameObject.transform.position, transform.rotation);
            Destroy(gameObject);
        }
    
    }
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player"){
            inRange = true;
            
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.tag == "Player"){
            inRange = false;
            
        } 
    }  
}
