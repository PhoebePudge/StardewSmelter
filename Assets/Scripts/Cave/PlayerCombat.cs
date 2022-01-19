using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] Transform Weapon; 

    [SerializeField] GameObject Particle;

    public Mesh Sword;
    public Mesh Pickaxe;

    private Collider WeaponCollider; 

    bool attacking = false;  

    private float swingSpeed = 2f;

    private weaponTypes weaponType = weaponTypes.Sword;

    public Vector3 defaultRotation;
    enum weaponTypes { Sword, Pickaxe};
    private void Start() {
        WeaponCollider = GetComponent<Collider>();
        WeaponCollider.enabled = false;
    }
    void Update()
    {
        if (weaponType == weaponTypes.Sword) {
            gameObject.GetComponent<MeshFilter>().mesh = Sword;
        } else {
            gameObject.GetComponent<MeshFilter>().mesh = Pickaxe;
        }

        if (Input.GetMouseButtonDown(0)) {
            Debug.LogError("s");
            weaponType = weaponTypes.Sword;
            Particle.SetActive(true);

            Particle.GetComponent<ParticleSystem>().Play();

            StartCoroutine(SwingWeapon());
        }
        if (Input.GetMouseButtonDown(2)) {
            //mine block in fount
            Debug.LogError("s");
            weaponType = weaponTypes.Pickaxe;
            Particle.SetActive(true);

            Particle.GetComponent<ParticleSystem>().Play();

            StartCoroutine(SwingPickaxe());
        }

        if (attacking == false) {
            Weapon.localRotation = Quaternion.Lerp(Weapon.localRotation, Quaternion.Euler(defaultRotation), Time.deltaTime * 2f * swingSpeed);
        }
    } 
    IEnumerator SwingWeapon() {
        attacking = true;
        WeaponCollider.enabled = true;
        Quaternion startRotation = Quaternion.Euler(0, 20, 0);
        defaultRotation = startRotation.eulerAngles;
        Weapon.localRotation = startRotation;
        Quaternion endRotation = Quaternion.Euler(0, -50, 0);
        float time = 0;
        while (Weapon.localRotation != endRotation) {
            time += Time.deltaTime;
            Weapon.localRotation = Quaternion.Lerp(Weapon.localRotation, endRotation, time * swingSpeed);
            if (Mathf.Round(Weapon.localRotation.eulerAngles.y / 10f) == Mathf.Round(endRotation.eulerAngles.y / 10f)) {
                Particle.GetComponent<ParticleSystem>().Stop();
                WeaponCollider.enabled = false;
            }
            yield return new WaitForFixedUpdate();
        }
        attacking = false;
        Debug.Log("done");
    }

    IEnumerator SwingPickaxe() {
        attacking = true;
        WeaponCollider.enabled = true;
        Quaternion startRotation = Quaternion.Euler(-60, 0, 0);
        defaultRotation = startRotation.eulerAngles;
        Weapon.localRotation = startRotation;
        Quaternion endRotation = Quaternion.Euler(40, -30, -30);
        float time = 0;
        while (Weapon.localRotation != endRotation) {
            time += Time.deltaTime;
            Weapon.localRotation = Quaternion.Lerp(Weapon.localRotation, endRotation, time * swingSpeed);
            if (Mathf.Round(Weapon.localRotation.eulerAngles.x / 10f) == Mathf.Round(endRotation.eulerAngles.x / 10f)) {
                Particle.GetComponent<ParticleSystem>().Stop();
                WeaponCollider.enabled = false;
            }
            yield return new WaitForFixedUpdate();
        }
        attacking = false;
        Debug.Log("done");
    }
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.name != "Player" & other.gameObject.name != "CaveFloor") {
            //Debug.LogError("oi you hit something called " + other.gameObject.name);
        }
        if (other.tag == "Mineable") { 
            //add to inventory
            GameObject.Destroy(other);
            
        }
        if (weaponType == weaponTypes.Sword) { 
            if (other.GetComponent<MonsterType>()) {
                other.GetComponent<MonsterType>().Damange(2);
            }
        }
    }
}
