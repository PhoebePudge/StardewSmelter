using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] Transform Weapon;

    [SerializeField] GameObject Particle;

    [SerializeField] Animator animator;

    private Quaternion defaultRotation;

    private void Start()
    {
        defaultRotation = Weapon.rotation;
    }
    void Update()
    {
    }
    private void SwitchAnimState()
    {
        animator.SetBool("attacking", false);
    }
}
