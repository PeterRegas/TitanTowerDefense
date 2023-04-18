using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinTower : MonoBehaviour
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
    public AudioSource cannon;
    private int j = 0;
    private GameObject target;
    public int towerLevel = 1;
    private float newfireRate;
    public float damage;
    void FixedUpdate(){
        newfireRate = fireRate - towerLevel*2.5f;
        if(target!=null){
            if(Vector3.Distance(target.GetComponent<Transform>().position, transform.position)>range || target.tag == "dead"){
                target = null;
            }
        }
        if(GameObject.FindGameObjectsWithTag("enemy") != null){
            enemies = GameObject.FindGameObjectsWithTag("enemy");
            foreach(GameObject i in enemies){
                distance = Vector3.Distance(i.GetComponent<Transform>().position, transform.position);
                if(distance<range){
                    if(target == null){
                        target = i;
                    }
                    if(i.GetComponent<EnemyMovement>().distanceToExit < target.GetComponent<EnemyMovement>().distanceToExit){
                        target = i;
                    }
                }
            }
            if(target != null){
                distance = Vector3.Distance(target.GetComponent<Transform>().position, transform.position);
                if(distance<=range){
                    Vector3 targetDirection = (target.transform.position - gun.position).normalized;
                    gun.Rotate(0,1f,0);
                    if(j >= newfireRate){
                         j=0;
                        shoot();
                    }
                }
            }
        }
        //Change color based on level
        if(towerLevel == 2){
            GetComponent<Renderer>().material.color = Color.green;
        }
        if(towerLevel == 3){
            GetComponent<Renderer>().material.color = Color.yellow;
        }
        if(towerLevel == 4){
            GetComponent<Renderer>().material.color = Color.red;
        }
        if(towerLevel == 5){
            GetComponent<Renderer>().material.color = Color.magenta;
        }
        j++;
    }
    void shoot(){
        GameObject bulletClone = Instantiate(bullet, barrel.position, transform.rotation);
        cannon.Play();
        bulletClone.GetComponent<Bullet>().damage = damage+(towerLevel*5    );
        bulletClone.GetComponent<Rigidbody>().AddForce(gun.transform.forward * shotSpeed);
        Destroy(bulletClone,10);
    }

    public void Upgrade(){
        towerLevel++;
        
        
    }

}
