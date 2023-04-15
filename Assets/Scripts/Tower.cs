using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private GameObject[] enemies;
    private float distance;
    [SerializeField] float range;
    [SerializeField] float shotSpeed;
    [SerializeField] float fireRate;
    [SerializeField] Transform gun;
    [SerializeField] Transform barrel;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] GameObject bullet;
    private int j = 0;
    private GameObject target;
    public int towerLevel = 1;
    private float newfireRate;
    private float damage;

    // Update is called once per frame
    void FixedUpdate()
    {
        newfireRate = fireRate - towerLevel*1.5f;
        enemies = GameObject.FindGameObjectsWithTag("enemy");
        if(enemies!=null){
            foreach(GameObject i in enemies){
                distance = Vector3.Distance(i.GetComponent<Transform>().position, transform.position);
                if(distance<=range){
                    if(target == null){
                        target = i;
                    }
                    if(i.GetComponent<EnemyMovement>().distanceToTarget < target.GetComponent<EnemyMovement>().distanceToTarget){
                        target = i;
                    }
                }
            }
            if(target!=null){
                distance = Vector3.Distance(target.GetComponent<Transform>().position, transform.position);
                if(distance<=range){
                    Vector3 targetDirection = (target.transform.position - gun.position).normalized;
                    if(!Physics.Raycast(gun.position, targetDirection, distance, wallLayer)) {
                        gun.LookAt(target.GetComponent<Transform>().position);
                        gun.transform.Rotate(1f,1f,1f);
                        j++;
                        
                        if(j >= newfireRate){
                            j=0;
                            shoot();
                        }
                    }
                }
            }

        }

        
    }
    void shoot(){
        GameObject bulletClone = Instantiate(bullet, barrel.position, transform.rotation);
        bulletClone.GetComponent<Bullet>().damage = damage+(towerLevel*2);
        bulletClone.GetComponent<Rigidbody>().AddForce(gun.transform.forward * shotSpeed);
        Destroy(bulletClone,10);
    }
}
