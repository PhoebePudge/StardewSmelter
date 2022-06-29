using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// List of monster varients,
/// with appropirate chance in variables to reflect monster type
/// </summary>
namespace Monsters {
    public class Dragonfly : MonsterType {
        public override void Start() {
            gameObject.name = "Dragonfly";

            GameObject child = Instantiate(Resources.Load("Cave/Dragonfly") as GameObject);

            child.transform.SetParent(transform);
            child.transform.localPosition = new Vector3(0, 1, 0);
            child.transform.rotation = Quaternion.Euler(-90, -90, -90);
            followActivationDistance = 0f;
            attackActivationDistance = .2f;

            Speed = 5;
            AngularSpeed = 500;

            base.Start();
        }
    }
    public class Snake : MonsterType {
        public override void Start() {
            gameObject.name = "Snake";

            GameObject child = Instantiate(Resources.Load("Cave/Snake") as GameObject);

            child.transform.SetParent(transform);
            child.transform.localPosition = new Vector3(0, .5f, 0);
            child.transform.rotation = Quaternion.Euler(-90, 180, 0);
            followActivationDistance = 0f;
            attackActivationDistance = 0f;

            Speed = 5;
            AngularSpeed = 500;

            base.Start();
        }
    }
    public class Bee : MonsterType {
        public override void Start() {
            gameObject.name = "Bee";

            GameObject child = Instantiate(Resources.Load("Cave/Bee") as GameObject);

            child.transform.SetParent(transform);
            child.transform.localPosition = new Vector3(0, 1, 0);
            child.transform.rotation = Quaternion.Euler(-90, -90, -90);

            followActivationDistance = 0f;
            attackActivationDistance = .2f;

            Speed = 5;
            AngularSpeed = 500;

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
            gameObject.name = "Cyclopse";

            maxHealth = 80f;

            GameObject child = Instantiate(Resources.Load("Cave/Cyclopse") as GameObject);

            child.transform.SetParent(transform);
            child.transform.localPosition = Vector3.zero;

            animator = child.GetComponent<Animator>();

            deathFX = Resources.Load("CyclopsDeathFX") as GameObject;

            base.Start();
        }
    }
    public class Goblin : MonsterType {
        public override void Start() {
            gameObject.name = "Goblin";

            GameObject child = Instantiate(Resources.Load("Cave/Goblin") as GameObject);

            child.transform.SetParent(transform);
            child.transform.localPosition = Vector3.zero;
            child.transform.rotation = Quaternion.Euler(0, 0, 0);

            animator = child.GetComponent<Animator>();

            followActivationDistance = 0f;
            attackActivationDistance = .2f;

            Speed = 5;
            AngularSpeed = 500;

            base.Start();
        }
    }
    public class GoblinThrower : MonsterType {
        public override void Start() {
            gameObject.name = "GoblinThrower";

            GameObject child = Instantiate(Resources.Load("Cave/GoblinThrower") as GameObject);

            child.transform.SetParent(transform);
            child.transform.localPosition = Vector3.zero;
            animator = child.GetComponent<Animator>();

            followActivationDistance = 0f;
            attackActivationDistance = .2f;

            Speed = 5;
            AngularSpeed = 500;

            base.Start();
        }
    }
    public class GoblinBoss : MonsterType {
        public override void Start() {
            gameObject.name = "Goblin Boss";

            GameObject child = Instantiate(Resources.Load("Cave/GoblinBoss") as GameObject);

            child.transform.SetParent(transform);
            child.transform.localPosition = Vector3.zero;
            child.transform.rotation = Quaternion.Euler(0, 0, 0);
            animator = child.GetComponent<Animator>();

            followActivationDistance = 0f;
            attackActivationDistance = .2f;

            Speed = 5;
            AngularSpeed = 500;

            base.Start();
        }
    }
    public class Giant : MonsterType {
        public override void Start() {
            gameObject.name = "Giant";

            GameObject child = Instantiate(Resources.Load("Cave/Giant") as GameObject);

            child.transform.SetParent(transform);
            child.transform.localPosition = Vector3.zero;
            animator = child.GetComponent<Animator>();

            followActivationDistance = 0f;
            attackActivationDistance = .2f;

            Speed = 5;
            AngularSpeed = 500;

            base.Start();
        }
    }
    public class CrystalGiant : MonsterType {
        public override void Start() {
            gameObject.name = "Crystal Giant";

            GameObject child = Instantiate(Resources.Load("Cave/CrystalGiant") as GameObject);

            child.transform.SetParent(transform);
            child.transform.localPosition = Vector3.zero;
            animator = child.GetComponent<Animator>();

            followActivationDistance = 0f;
            attackActivationDistance = .2f;

            Speed = 5;
            AngularSpeed = 500;
            base.Start();
        }
    }
    public class RockGolem : MonsterType {
        public override void Start() {
            gameObject.name = "Rock Golem";

            GameObject child = Instantiate(Resources.Load("Cave/RockGolem") as GameObject);

            child.transform.SetParent(transform);
            child.transform.localPosition = Vector3.zero;
            animator = child.GetComponent<Animator>();

            followActivationDistance = 0f;
            attackActivationDistance = .2f;

            Speed = 5;
            AngularSpeed = 500;
            base.Start();
        }
    }
}
