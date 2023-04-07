using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyMovement : MonoBehaviour {
    [SerializeField] Transform[] waypoints;
    [SerializeField] float closeEnoughDistance;
    public NavMeshAgent agent = null;
    public Slider healthslide;
    public float distanceToTarget;
    public float health = 100f;
    public float MaxHealth=100f;
    //[SerializeField] Animator animator = null;
    public int wayPointIndex = 0;
    
    private void Start() {
        if (wayPointIndex==0) {
            wayPointIndex++;
        }
    }

    private void FixedUpdate() {

        healthslide.value = health/MaxHealth;
        if(health<=0){
            Destroy(gameObject);
            return;

        }
        agent.SetDestination(waypoints[wayPointIndex].position);
        distanceToTarget = Vector3.Distance(agent.transform.position, waypoints[wayPointIndex].position);
        if (distanceToTarget < closeEnoughDistance) {
            // make the next waypoint active
            wayPointIndex++;
        }
        // navigate to the waypoint
        agent.SetDestination(waypoints[wayPointIndex].position);
        //animator.SetFloat("Forward", agent.velocity.magnitude);
    }
    void OnTriggerEnter(Collider thing)
    {
        if(thing.tag == "Exit"){
            //thing.GetComponent<enemy>().Hit(damage);
            Destroy(gameObject);
        }
        
    }
    
}