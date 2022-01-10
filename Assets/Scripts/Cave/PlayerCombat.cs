using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] Transform Weapon;
    [SerializeField] GameObject Particle;
    private Collider WeaponCollider;
    bool attacking = false;
    private float swingSpeed = 2f;
    private void Start() {
        WeaponCollider = GetComponent<Collider>();
        WeaponCollider.enabled = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) {
            Debug.LogError("s");
            Particle.SetActive(true);
            StartCoroutine(SwingWeapon());
        }
        if (attacking == false) {
            Weapon.localRotation = Quaternion.Lerp(Weapon.localRotation, Quaternion.Euler(0, 20, 0), Time.deltaTime * 2f * swingSpeed);
        }
    } 
    IEnumerator SwingWeapon() {
        attacking = true;
        WeaponCollider.enabled = true;
        Quaternion startRotation = Quaternion.Euler(0, 20, 0);
        Weapon.localRotation = startRotation;
        Quaternion endRotation = Quaternion.Euler(0, -50, 0);
        float time = 0;
        while (Weapon.localRotation != endRotation) {
            time += Time.deltaTime;
            Weapon.localRotation = Quaternion.Lerp(Weapon.localRotation, endRotation, time * swingSpeed);
            if (Mathf.Round(Weapon.localRotation.eulerAngles.y / 10f) == Mathf.Round(endRotation.eulerAngles.y / 10f)) {
                Particle.SetActive(false);
                WeaponCollider.enabled = false;
            }
            yield return new WaitForFixedUpdate();
        }
        attacking = false;
        Debug.Log("done");
    }
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.name != "Player") {
            //Debug.LogError("oi you hit something called " + other.gameObject.name);
        }

        if (other.GetComponent<MonsterType>()) {
            other.GetComponent<MonsterType>().Damange(2);
        }
    }
}
