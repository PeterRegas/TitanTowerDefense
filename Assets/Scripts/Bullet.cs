using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject levelcontrol;
    public int damage;
    private void Start() {
        levelcontrol = GameObject.FindGameObjectWithTag("levelcontrol");
    }
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "enemy"){
            other.GetComponent<EnemyMovement>().health -= damage;
            if(other.GetComponent<EnemyMovement>().health<=0){
                levelcontrol.GetComponent<LevelControls>().Money+=1;
                Destroy(other);
            }
        }
    }
}
