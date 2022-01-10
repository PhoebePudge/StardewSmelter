using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace Monsters {
    public class Rat : MonsterType {
        public override void Start() {
            name = "Rat";
            gameObject.GetComponent<MeshFilter>().mesh = Resources.Load<Mesh>("rat");
            gameObject.GetComponent<MeshRenderer>().material = Resources.Load<Material>("loadthisrat");
            followActivationDistance = 0f;
            attackActivationDistance = 1f;

            base.Start();
        }
    }
    public class Bug : MonsterType {
        public override void Start() {
            name = "Bug";
            base.Start();

            GameObject child = Instantiate(Resources.Load("Skeleton") as GameObject);
            child.transform.SetParent(transform); 
            child.transform.localPosition = Vector3.zero;
            child.AddComponent<Animator>().runtimeAnimatorController = Resources.Load("SkeletonAnim") as RuntimeAnimatorController;
            animator = child.GetComponent<Animator>(); 
        }
    }
    public class Skeleton : MonsterType {
        public override void Start() {
            name = "Skeleton"; 
            
            base.Start();

            GameObject child = Instantiate(Resources.Load("Skeleton") as GameObject);
            child.transform.SetParent(transform);
            child.transform.localPosition = Vector3.zero;
            child.AddComponent<Animator>().runtimeAnimatorController = Resources.Load("SkeletonAnim") as RuntimeAnimatorController;
            animator = child.GetComponent<Animator>();
        }
    }
}
public class MonsterType : MonoBehaviour{
    protected string name; 
    protected float followActivationDistance = 4f;  
    protected float attackActivationDistance = 1.5f;  
    private Transform player;
    private NavMeshAgent agent;
    //state variables
    protected enum EnemyStates {idle, follow, attack};
    protected EnemyStates state = EnemyStates.idle;
    private EnemyStates previousState = EnemyStates.idle;

    //idle wander variables
    float idleRadius = 20f;
    float idleTimer = 4f;
    float timer;

    protected Animator animator = null;
    public void Damange(int damage, float knockbackStrength = 500f) {
        state = EnemyStates.idle;
        Vector3 moveDirection = player.transform.position - transform.position;
        gameObject.GetComponent<Rigidbody>().AddForce(moveDirection.normalized * -knockbackStrength);

        gameObject.GetComponent<Renderer>().material.color = Color.magenta;
    }
    public override string ToString() {
        return name;
    } 
    public virtual string GetName() {
        return name;
    } 
    public virtual void Start() {
        idleTimer = Random.Range(2f, 6f); 
        gameObject.name = name; 
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform; 
    }

    public virtual void Update() {
        //get distance
        float dist = Vector2.Distance(gameObject.transform.position,player.transform.position);

        //update states
        if (dist < attackActivationDistance) {
            state = EnemyStates.attack;
        }else if (dist < followActivationDistance) {
            state = EnemyStates.follow;
        }else {
            state = EnemyStates.idle;
        }

        //switch statement when new state is selected
        if (previousState != state) {  
            switch (state) {
                case EnemyStates.idle:
                    agent.Resume();
                    if (animator != null) {
                        animator.SetBool("Walking", false);
                        animator.SetBool("Attacking", false);
                    }
                    break;
                case EnemyStates.follow:
                    agent.Resume();
                    if (animator != null) {
                        animator.SetBool("Walking", true);
                        animator.SetBool("Attacking", false);
                    }
                    agent.SetDestination(player.position);
                    break;
                case EnemyStates.attack:
                    if (animator != null) {
                        animator.SetBool("Walking", false);
                        animator.SetBool("Attacking", true);
                    }
                    agent.Stop();
                    break;
                default:
                    break;
            }
        }

        //switch statement each update 
        switch (state) {
            case EnemyStates.idle: 
                timer += Time.deltaTime;
                if (timer >= idleTimer) {
                    Vector3 newPos = RandomNavSphere(idleRadius, -1);
                    agent.SetDestination(newPos);
                    timer = 0;
                }
                break;
            case EnemyStates.follow: 
                break;
            case EnemyStates.attack: 
                break;
            default:
                break;
        }


        //set previous state for value checking
        previousState = state;
    }
    private Vector3 RandomNavSphere(float dist, int layermask) {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += transform.position;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }
    private void OnDrawGizmos() {  
        switch (state) {
            case EnemyStates.idle:
                Gizmos.color = Color.green;
                break;
            case EnemyStates.follow:
                Gizmos.color = Color.yellow;
                break;
            case EnemyStates.attack:
                Gizmos.color = Color.red;
                break;
            default:
                break;
        }

        //draw a forward direction line 
        Gizmos.DrawLine(transform.position, transform.position + (transform.forward * 2f));
    }
}