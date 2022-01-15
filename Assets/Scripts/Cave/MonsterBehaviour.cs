using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace Monsters {
    public class Rat : MonsterType {
        public override void Start() {
            name = "Rat";
            GameObject child = Instantiate(Resources.Load("rat") as GameObject);
            child.transform.SetParent(transform);
            child.transform.localPosition = Vector3.zero;
            followActivationDistance = 0f;
            attackActivationDistance = 1f;
            Speed = 5;
            AngularSpeed = 500;
            base.Start();
        }
    }
    public class Bug : MonsterType {
        public override void Start() {
            name = "Bug";
            base.Start();

            GameObject child = Instantiate(Resources.Load("bug") as GameObject);
            child.transform.SetParent(transform); 
            child.transform.localPosition = new Vector3(0, 1, 0);
            AngularSpeed = 500; 
        }
        public override void AttackPlayer() {
            GameObject gm = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            gm.transform.SetParent(transform);
            gm.transform.localPosition = Vector3.zero;
            gm.transform.localScale = new Vector3(.5f, .5f, .5f);
            StartCoroutine(lerpGameobject(gm));
            
        }
        IEnumerator lerpGameobject(GameObject gm) {
            gm.transform.LookAt(player.transform.position);
            for (int i = 0; i < 100; i++) { 
                gm.GetComponent<Renderer>().material.color = Color.red;
                gm.transform.position += transform.forward * (Time.deltaTime * 2f);
                yield return new WaitForEndOfFrame();
            }
            Destroy(gm);
            yield return new WaitForEndOfFrame();
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

    protected float health;
    protected float maxHealth = 4f;

    protected Transform player;
    private NavMeshAgent agent;
    //state variables
    protected enum EnemyStates {idle, follow, attack};
    protected EnemyStates state = EnemyStates.idle;
    private EnemyStates previousState = EnemyStates.idle;

    //idle wander variables
    float idleRadius = 20f;
    float idleTimer = 4f;
    float timer;
     
    Vector3 ds;

    protected float Speed = 3.5f;
    protected int AngularSpeed = 240;
    protected Animator animator = null;
    public void Damange(int damage, float knockbackStrength = 500f) {
        
        health -= damage;
        if (health <= 0) {
            Destroy(gameObject); 
        }

        state = EnemyStates.idle;
        Vector3 moveDirection = player.transform.position - transform.position;
        gameObject.GetComponent<Rigidbody>().AddForce(moveDirection.normalized * -knockbackStrength);

        if (animator != null) {
            animator.SetTrigger("Damaged");
        }
        StartCoroutine(FlashDamage(damage));
        
    }
    IEnumerator FlashDamage(int damage) {

        List<Color> colors = new List<Color>();
        foreach (Renderer item in gameObject.GetComponentsInChildren<Renderer>()) {
            colors.Add(item.material.color);
        }

        Color def = gameObject.GetComponent<Renderer>().material.color;

        for (int i = 0; i < damage; i++) {

            foreach (Renderer item in gameObject.GetComponentsInChildren<Renderer>()) {
                item.material.color = Color.red;
            }

            gameObject.GetComponent<Renderer>().material.color = Color.red;
            yield return new WaitForSeconds(.2f);

            int s = 0;
            foreach (Renderer item in gameObject.GetComponentsInChildren<Renderer>()) {
                item.material.color = colors[i];
                s++;
            }

            gameObject.GetComponent<Renderer>().material.color = def;
            yield return new WaitForSeconds(.2f);
        }

        yield return null;
    }
    public virtual string GetName() {
        return name;
    } 
    public virtual void Start() {
        
        health = maxHealth;
        idleTimer = Random.Range(2f, 6f); 
        gameObject.name = name;  
        player = GameObject.FindGameObjectWithTag("Player").transform;
         
        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position, out hit, 1.0f, NavMesh.AllAreas)) {
            ds = hit.position;
        }

        agent = gameObject.AddComponent<NavMeshAgent>(); 
        agent.SetDestination(hit.position);
        agent.angularSpeed = AngularSpeed;
        agent.speed = Speed;
    }
    public virtual void AttackPlayer() {
        Vector3 moveDirection = transform.position - player.transform.position;
        //player.GetComponent<Rigidbody>().AddForce(moveDirection.normalized * -500f);
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
                    }
                    break;
                case EnemyStates.follow:
                    agent.Resume();
                    if (animator != null) {
                        animator.SetBool("Walking", true); 
                    }
                    agent.SetDestination(player.position);
                    break;
                case EnemyStates.attack:
                    if (animator != null) { 
                        animator.SetTrigger("Attacking");
                        
                    }
                    AttackPlayer();
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

        if (agent.path.status != NavMeshPathStatus.PathComplete) { 
            state = EnemyStates.idle;
            
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
        Gizmos.DrawLine(transform.position, agent.destination); 
    }
}