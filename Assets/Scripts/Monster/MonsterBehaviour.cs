using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Monsters {

    public class Dragonfly : MonsterType {
        public override void Start() {

            //name
            gameObject.name = "Dragonfly";

            //instanciate prefab
            GameObject child = Instantiate(Resources.Load("Cave/Dragonfly") as GameObject);

            //set parent and location of child
            child.transform.SetParent(transform);
            child.transform.localPosition = new Vector3(0,1,0);
            child.transform.rotation = Quaternion.Euler(-90,-90,-90);
            //give it a custom follow and attack activate distance
            followActivationDistance = 0f;
            attackActivationDistance = .2f;

            //give it a custom speed and angular speed
            Speed = 5;
            AngularSpeed = 500;

            //call the basic start
            base.Start();
        }
    }
    public class Snake : MonsterType {
        public override void Start() {

            //name
            gameObject.name = "Snake";

            //instanciate prefab
            GameObject child = Instantiate(Resources.Load("Cave/Snake") as GameObject);

            //set parent and location of child 
            child.transform.SetParent(transform);
            child.transform.localPosition = new Vector3(0, .5f, 0);
            child.transform.rotation = Quaternion.Euler(-90, 180,0);
            //give it a custom follow and attack activate distance
            followActivationDistance = 0f;
            attackActivationDistance = 0f;

            //give it a custom speed and angular speed
            Speed = 5;
            AngularSpeed = 500;

            //call the basic start
            base.Start();
        } 
    }

    public class Bee : MonsterType {
        public override void Start() { 

            //name
            gameObject.name = "Bee";
            
            //instantiate a gameobject from the prefab
            GameObject child = Instantiate(Resources.Load("Cave/Bee") as GameObject);

            //set parent and location of child
            child.transform.SetParent(transform);
            child.transform.localPosition = new Vector3(0, 1, 0);
            child.transform.rotation = Quaternion.Euler(-90, -90, -90);

            //give it a custom follow and attack activate distance
            followActivationDistance = 0f;
            attackActivationDistance = .2f;

            //give it a custom speed and angular speed
            Speed = 5;
            AngularSpeed = 500;

            //call the basic start
            base.Start();
        }
        /*
        public override void AttackPlayer() {

            //custom attack function where balls are thrown at the player
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
        */
    }

    public class Cyclopse : MonsterType {
        public override void Start() {

            //name
            gameObject.name = "Cyclopse";

            //set a custom health
            maxHealth = 80f;

            //instantiate a gameobject from the prefab
            GameObject child = Instantiate(Resources.Load("Cave/Cyclopse") as GameObject);


            //set the parent and position of the child
            child.transform.SetParent(transform);
            child.transform.localPosition = Vector3.zero;

            //add our animator
            //Add animator to prefab please
            //child.AddComponent<Animator>().runtimeAnimatorController = Resources.Load("StrongAnimation") as RuntimeAnimatorController;
            animator = child.GetComponent<Animator>();

            deathFX = Resources.Load("CyclopsDeathFX") as GameObject;



            //call our base start function
            base.Start();
        }
    }

    public class Goblin : MonsterType
    {
        public override void Start()
        {

            //name
            gameObject.name = "Goblin";

            //instanciate prefab
            GameObject child = Instantiate(Resources.Load("Cave/Goblin") as GameObject);

            //set parent and location of child
            child.transform.SetParent(transform);
            child.transform.localPosition = Vector3.zero;
            child.transform.rotation = Quaternion.Euler(0,0,0);

            animator = child.GetComponent <Animator>();

            //give it a custom follow and attack activate distance
            followActivationDistance = 0f;
            attackActivationDistance = .2f;

            //give it a custom speed and angular speed
            Speed = 5;
            AngularSpeed = 500;

            //call the basic start
            base.Start();
        }
    }

    public class GoblinThrower : MonsterType
    {
        public override void Start()
        {

            //name
            gameObject.name = "GoblinThrower";

            //instanciate prefab
            GameObject child = Instantiate(Resources.Load("Cave/GoblinThrower") as GameObject);

            //set parent and location of child
            child.transform.SetParent(transform);
            child.transform.localPosition = Vector3.zero;
            //child.transform.rotation = Quaternion.Euler(-90, -90, -90);

            animator = child.GetComponent<Animator>();

            //give it a custom follow and attack activate distance
            followActivationDistance = 0f;
            attackActivationDistance = .2f;

            //give it a custom speed and angular speed
            Speed = 5;
            AngularSpeed = 500;

            //call the basic start
            base.Start();
        }
    }

    public class GoblinBoss : MonsterType
    {

    }

    public class Giant : MonsterType
    {
        public override void Start()
        {
            //name
            gameObject.name = "Giant";

            //instanciate prefab
            GameObject child = Instantiate(Resources.Load("Cave/Giant") as GameObject);

            //set parent and location of child
            child.transform.SetParent(transform);
            child.transform.localPosition = Vector3.zero;
            //child.transform.rotation = Quaternion.Euler(-90, -90, -90);

            animator = child.GetComponent<Animator>();

            //give it a custom follow and attack activate distance
            followActivationDistance = 0f;
            attackActivationDistance = .2f;

            //give it a custom speed and angular speed
            Speed = 5;
            AngularSpeed = 500;

            //call the basic start
            base.Start();
        }
    }

    public class CrystalGiant : MonsterType
    {

    }

    public class RockGolem : MonsterType
    {

    }
}
public class MonsterType : MonoBehaviour{

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
    protected enum EnemyStates { idle, alert, follow, attack, damaged, dead };
    [SerializeField] protected EnemyStates state = EnemyStates.idle;
    private EnemyStates previousState = EnemyStates.idle;

    //idle wander variables
    [SerializeField] float idleRadius = 20f;
    [SerializeField] float idleTimer = 4f;
    [SerializeField] float timer;

    //speed settings
    [SerializeField] protected float Speed = 3.5f;
    [SerializeField] protected int AngularSpeed = 240;

    [SerializeField] private float followActivationTime = .5f;

    //protected Transform knockback;

    //GameObject Knockback(GameObject obj, string name)
    //{
    //    Transform trans = obj.transform;
    //    Transform childTrans = trans.Find(name);
    //    if (childTrans != null)
    //    {
    //        return childTrans.gameObject;
    //    }
    //    else
    //    {
    //        return null;
    //    }
    //}

    //animator

    [SerializeField] protected Animator animator = null;
    public void Damage(int damage, float knockbackStrength = 10f) {

        BloodParticle.CreateSplatter(transform.position, damage);
        DamageIndicator.DisplayDamage(Camera.main.WorldToScreenPoint(transform.position), damage);

        //take damage away from our health
        health -= damage;

        //GetComponent<Rigi>

        //if our health is negative or 0, we will destroy the gameobject
        if (health <= 0) {
            //Destroy(gameObject); 
            state = EnemyStates.dead;
        }

        state = EnemyStates.idle;

        //create a backwards force to move the enemy away
        //Vector3 moveDirection = player.transform.position - transform.position;
        //gameObject.GetComponent<Rigidbody>().AddForce(moveDirection.normalized * -knockbackStrength);
        

        //play the animation
        if (animator != null) {
            animator.SetTrigger("Damaged");
        }

        //show a flashed damage
        StartCoroutine(FlashDamage(damage));

        //this.gameObject.transform.Equals(knockback);

    }
    private void OnDestroy() {
        //Instantiate(deathFX, transform.position, transform.rotation);
        //call any effects for when the enemy is destroyed
    }
    IEnumerator FlashDamage(int damage) {
        Debug.LogError("Stack here");
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

        //knockback = GameObject.FindGameObjectWithTag("Knockback").transform;

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
    public virtual void AttackPlayer() { 
        Vector3 moveDirection = transform.position - player.transform.position;
        HealthSlider.Damage(1);
        //player.GetComponent<Rigidbody>().AddForce(moveDirection.normalized * -500f);
    }
    public virtual void Update() {

        //get distance
        //Debug.Log(gameObject.transform.position);
        //Debug.Log(player.transform.position);
        float dist = Vector2.Distance(gameObject.transform.position, player.transform.position);

        //update states
        if (dist < attackActivationDistance) {
            state = EnemyStates.attack;
        }else if (dist < followActivationDistance) {
            state = EnemyStates.alert;
        }else {
            state = EnemyStates.idle;
        }
        
        //switch statement when new state is selected
        if (previousState != state) {
            OnStateChange();
        }

        //switch statement each update 
        switch (state) {
            case EnemyStates.idle: 

                //wander timer for the enemy
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
    private void OnStateChange()
    { 
        switch (state)
        {
            case EnemyStates.alert:
                StartCoroutine(AlertTime());
            break;
            case EnemyStates.idle:

                agent.isStopped = false;
                if (animator != null)
                {
                    animator.SetBool("Walk", false);
                }
                break;
            case EnemyStates.follow:

                agent.isStopped = false;
                if (animator != null)
                {
                    animator.SetBool("Walk", true);
                }
                agent.SetDestination(player.position);
                break;
            case EnemyStates.attack:
                if (animator != null)
                {
                    animator.SetTrigger("Attack");
                }
                AttackPlayer();
                agent.isStopped = true;
                break;
            default:
                break;
        }
    }
    IEnumerator AlertTime()
    {
        Debug.LogError("Stack here");
        yield return new WaitForSeconds(followActivationTime);

        state = EnemyStates.follow;
    }
    private Vector3 RandomNavSphere(float dist, int layermask) {
        //chose a random direction, and use it to select a random position within the navmesh
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