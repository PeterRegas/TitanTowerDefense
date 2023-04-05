using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {
    [SerializeField] Transform[] waypoints;
    [SerializeField] float closeEnoughDistance;
    public NavMeshAgent agent = null;
    public float distanceToTarget;
    //[SerializeField] Animator animator = null;
    public int wayPointIndex = 0;
    
    private void Start() {
        if (wayPointIndex==0) {
            wayPointIndex++;
        }
    }

    private void FixedUpdate() {
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