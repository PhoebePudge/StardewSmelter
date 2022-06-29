using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Enemy : MonoBehaviour
{

    //HP

    public int enemyHP;

    //DAM

    public int enemyDam;

    //FX
    [SerializeField]
    private AudioSource deathSFX;

    public Animation enemyAnimator;
    public Collider enemyCollider;
    private bool hasAttacked;
    private int attackCountdown;

    public string enemyName;

    [SerializeField]
    private GameObject enemyDrop;

    [SerializeField]
    private GameObject killEffect;

    private void Start()
    {
        enemyName = this.gameObject.name;        
    }

    //private void DestroyObject()
    //{
    //    if (enemyName == ("default"))
    //    {
    //        // Need to know where our list is
    //        Destroy(this.gameObject);
            
    //        Instantiate(killEffect, transform.position, transform.rotation);
    //    }
    //}

    private void OnDestroy()
    {
        Instantiate(enemyDrop, transform.position, transform.rotation);
        Instantiate(killEffect, transform.position, transform.rotation);
    }
}
