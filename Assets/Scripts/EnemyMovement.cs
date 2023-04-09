using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyMovement : MonoBehaviour {
    [SerializeField] Transform[] waypoints;
    [SerializeField] float closeEnoughDistance;
    [SerializeField] float speed;
    [SerializeField] Transform player;
    public NavMeshAgent agent = null;
    public Slider healthslide;
    public Canvas healthslideCanvas;
    public float distanceToTarget;
    public float health = 100f;
    public float MaxHealth=100f;
    //[SerializeField] Animator animator = null;
    public int wayPointIndex = 0;

    [SerializeField] private Animator animator;
    
    private void Start() {
        if (wayPointIndex==0) {
            wayPointIndex++;
        }
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void FixedUpdate() {

        healthslide.value = health/MaxHealth;
        if(health<=0){
            agent.speed = 0;
            animator.SetTrigger("dying");
            
            Destroy(gameObject, 3.5f);
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
        agent.speed = speed;
        animator.SetFloat("speed", agent.speed);
    }
    void OnTriggerEnter(Collider thing)
    {
        if(thing.tag == "Exit"){
            Debug.Log("Exit");
            //thing.GetComponent<enemy>().Hit(damage);
            Destroy(gameObject);
        }
        
    }
    void Update(){
        healthslideCanvas.GetComponent<Transform>().transform.LookAt(player);
    }
    
    
}