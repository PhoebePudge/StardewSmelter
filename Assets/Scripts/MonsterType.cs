using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// Monster template that all monster variations are based from
/// </summary>
public class MonsterType : MonoBehaviour {

    protected GameObject deathFX;
    protected GameObject damageFX;

    //follow and attack distance
    [SerializeField] protected float followActivationDistance = 4f;
    [SerializeField] protected float attackActivationDistance = .5f;

    //health data
    [SerializeField] protected float health;
    [SerializeField] protected float maxHealth = 4f;

    //player transform
    protected Transform player;

    //agent data
    private NavMeshAgent agent;

    //state variables
    protected enum EnemyStates { idle, alert, follow, attack, damaged, dead, none };
    [SerializeField] protected EnemyStates state;
    private EnemyStates previousState = EnemyStates.none;

    //idle wander variables
    [SerializeField] float idleRadius = 20f;
    [SerializeField] float idleTimer = 4f;
    [SerializeField] float timer;

    //speed settings
    [SerializeField] protected float Speed = 3.5f;
    [SerializeField] protected int AngularSpeed = 240;

    [SerializeField] private float followActivationTime = .5f;

    //animator
    [SerializeField] protected Animator animator = null;

    public void Damage(int damage, float knockbackStrength = 10f) {
        //Give the monster damage
        BloodParticle.CreateSplatter(transform.position, damage);
        DamageIndicator.DisplayDamage(Camera.main.WorldToScreenPoint(transform.position), damage);

        //take damage away from our health
        health -= damage;

        //if our health is negative or 0, we will destroy the gameobject
        if (health <= 0) {
            Destroy(gameObject);
            state = EnemyStates.dead;
        }

        state = EnemyStates.idle;

        //play the animation
        if (animator != null) {
            animator.SetTrigger("Damaged");
        }

        //show a flashed damage
        StartCoroutine(FlashDamage(damage));
    }
    IEnumerator FlashDamage(int damage) {
        //Flash damage colour
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
    public virtual void Start() {
        //set our health to be our max ammount
        health = maxHealth;

        //set our idle wander timer to be random
        idleTimer = Random.Range(6f, 10f);

        //set our player transform reference
        player = GameObject.FindGameObjectWithTag("Player").transform;

        //set our agent transform reference
        agent = gameObject.AddComponent<NavMeshAgent>();

        //set our angular and regular speed
        agent.angularSpeed = AngularSpeed;
        agent.speed = Speed;

        //set our navmesh destination to a close position (this was to deal with a issue in which the agents teleport away)
        NavMeshHit hit;
        if (NavMesh.SamplePosition(transform.position, out hit, 1.0f, NavMesh.AllAreas)) {
            agent.SetDestination(hit.position);
        }
    }
    public virtual void Update() {

        //compare player distance and switch states dependant on that
        float dist = Vector2.Distance(gameObject.transform.position, player.transform.position);

        if (dist < attackActivationDistance) {
            state = EnemyStates.attack;
        } else if (dist < followActivationDistance) {
            state = EnemyStates.alert;
        } else {
            state = EnemyStates.idle;
        }

        //check if we switch states
        if (previousState != state) {
            OnStateChange(state);
        }

        //update for each state
        switch (state) {
            case EnemyStates.idle:
                //set idle walk places
                timer += Time.deltaTime;
                if (timer >= idleTimer) {
                    Vector3 newPos = RandomNavSphere(idleRadius, -1);
                    agent.SetDestination(newPos);
                    timer = 0;
                }

                //fix animations dependant on how close enemy is to target
                if (Vector3.Distance(transform.position, agent.destination) > 1.0f) {
                    animator.SetBool("Walk", true);
                } else {
                    animator.SetBool("Walk", false);
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
        previousState = state;

    }
    private void OnStateChange(EnemyStates current) {
        switch (current) {
            case EnemyStates.alert:
                StartCoroutine(AlertTime());
                break;
            case EnemyStates.idle: 
                agent.isStopped = false;
                if (animator != null) {
                    animator.SetBool("Walk", false);
                }
                break;
            case EnemyStates.follow:

                agent.isStopped = false;
                if (animator != null) {
                    animator.SetBool("Walk", true);
                }
                agent.SetDestination(player.position);
                break;
            case EnemyStates.attack:
                if (animator != null) {
                    animator.SetTrigger("Attack");
                }
                agent.isStopped = true;
                break;
            case EnemyStates.damaged:
                if (animator != null) {
                    animator.SetTrigger("Damaged");
                }
                break;
            case EnemyStates.dead:
                if (animator != null) {
                    animator.SetBool("Death", true);
                }
                break; 
            default:
                break;
        }
    }
    IEnumerator AlertTime() {
        //be in alert state for breif time, then switch to follow state
        yield return new WaitForSeconds(followActivationTime);

        state = EnemyStates.follow;
    }
    private Vector3 RandomNavSphere(float dist, int layermask) {
        //find a random position from target at a distance
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += transform.position;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }
    private void OnDrawGizmos() {
        //change the colour dependant on the state
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
        //draw a line to the destination
        Gizmos.DrawLine(transform.position, agent.destination);
    }
}