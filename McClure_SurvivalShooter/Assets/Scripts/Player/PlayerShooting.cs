﻿using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;
    //Shot damage multiplyer affected by macho
    public int damageMulti = 1;
    public float timeBetweenBullets = 0.15f;
    public float range = 5f;


    float timer;
    Ray shootRay = new Ray();
    RaycastHit shootHit;
    int shootableMask;
    ParticleSystem gunParticles;
    LineRenderer gunLine;
    AudioSource gunAudio;
    Light gunLight;
    float effectsDisplayTime = 0.2f;

    GameObject player;
    PlayerMacho playerMacho;

    bool enemyInRange = false;
    //GameObject enemy;


    void Awake ()
    {
        shootableMask = LayerMask.GetMask ("Shootable");
        gunParticles = GetComponent<ParticleSystem> ();
        gunLine = GetComponent <LineRenderer> ();
        gunAudio = GetComponent<AudioSource> ();
        gunLight = GetComponent<Light> ();
        player = GameObject.FindGameObjectWithTag("Player");
        playerMacho = player.GetComponent<PlayerMacho>();

        //enemy = GameObject.FindGameObjectWithTag("Enemy");
    }


    void Update ()
    {
        timer += Time.deltaTime;

        //enemy = GameObject.FindGameObjectWithTag("Enemy");

        if (Input.GetButton ("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0 && enemyInRange == true)
        {
            Shoot ();
        }

        if(timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects ();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemyInRange = true;
            print(enemyInRange);
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemyInRange = false;
            print(enemyInRange);
        }
    }


    public void DisableEffects ()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }


    void Shoot ()
    {
        timer = 0f;

        gunAudio.Play ();



        gunLight.enabled = true;

        gunParticles.Stop ();
        gunParticles.Play ();

        gunLine.enabled = true;
        gunLine.SetPosition (0, transform.position);

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        if(Physics.Raycast (shootRay, out shootHit, range, shootableMask))
        {
            EnemyHealth enemyHealth = shootHit.collider.GetComponent <EnemyHealth> ();
            if(enemyHealth != null)
            {

                if (playerMacho.isMacho == true)
                {
                    damageMulti = 2;
                }
                else
                {
                    damageMulti = 1;
                }

                //enemyHealth.TakeDamage (damagePerShot * damageMulti, shootHit.point);
            }
            gunLine.SetPosition (1, shootHit.point);
        }
        else
        {
            gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
        }
    }
}
