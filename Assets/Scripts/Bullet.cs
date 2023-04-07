using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "enemy"){
            other.GetComponent<EnemyMovement>().health -= damage;
            if(other.GetComponent<EnemyMovement>().health<=0){
                Destroy(other);
            }
        }
    }
}
