using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Monsters {
    public class Slime : MonsterType {
        public override void Start() {
            name = "Slime";
            colour = Color.green;
            base.Start();
        }
    }
    public class Bug : MonsterType {
        public override void Start() {
            name = "Bug";
            colour = Color.magenta;
            base.Start();
        }
    }
    public class Skeleton : MonsterType {
        public override void Start() {
            name = "Skeleton";
            colour = Color.grey;
            base.Start();
        }
    }
}
public class MonsterType : MonoBehaviour{
    protected string name;
    protected Color colour;

    //Movement variables
    protected float MoveSpeed = 4;
    protected float MaxDist = 10;
    protected float MinDist = 5;

    //Player instance
    private Transform player;


    public override string ToString() {
        return name;
    } 
    public virtual string GetName() {
        return name;
    }
    public virtual Color GetColour() {
        return colour;
    } 
    public virtual void Start() {
        gameObject.name = name;
        gameObject.GetComponent<MeshRenderer>().material.color = colour;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        gameObject.AddComponent<Rigidbody>();
    }
    
    public virtual void Update() {
        transform.LookAt(player);
        if (Physics.Raycast(gameObject.transform.position, transform.forward, 2f)) {

        } else {
            if (Vector3.Distance(transform.position, player.position) >= MinDist) {

                transform.position += transform.forward * MoveSpeed * Time.deltaTime;

                if (Vector3.Distance(transform.position, player.position) <= MaxDist) {
                    //Here Call any function U want Like Shoot at here or something
                }

            }
        }
    }
}