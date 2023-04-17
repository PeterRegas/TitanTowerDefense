using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject levelcontrol;
    public float damage;
    private void Start() {
        levelcontrol = GameObject.FindGameObjectWithTag("levelcontrol");
        Destroy(gameObject, 5f);
    }
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "enemy"){
            other.GetComponent<EnemyMovement>().health -= damage;
            if(other.GetComponent<EnemyMovement>().health<=0){
                levelcontrol.GetComponent<LevelControls>().Money+=10;
                Destroy(other);
            }
            Destroy(gameObject);
        }
    }
}
