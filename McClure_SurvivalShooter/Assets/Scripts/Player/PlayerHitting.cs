using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitting : MonoBehaviour {

    public int damagePerHit = 20;
    //Macho mutiplier
    public int damageMulti = 1;

    public float timeBetweenHits = .125f;

    float timer;
    bool hitting;

    GameObject player;
    PlayerMacho playerMacho;
    //Collider enemy = null;

    List<Collider> enemies;

    Animator anim;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerMacho = player.GetComponent<PlayerMacho>();
        anim = GetComponent<Animator>();
        enemies = new List<Collider>();
    }

    // Update is called once per frame
    void Update () {
        timer += Time.deltaTime;
        if (Input.GetButton("Fire1") && timer >= timeBetweenHits)
        {
            hitting = true;
            anim.SetBool("IsHitting", hitting);
            Invoke("EndPunchAnim", 0.125f);
            for (int i = 0; i < enemies.Count; i++)
            {
                Damage(enemies[i]);
            }
            timer = 0f;
        }
    }

    void EndPunchAnim()
    {
        anim.SetBool("IsHitting",false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            //enemy = other;

            enemies.Add(other);
            //enemyInRange = true;
            //print(enemyInRange);
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            //enemy = null;

            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i] == other)
                {
                    enemies.RemoveAt(i);
                    break;
                }
            }

            //enemyInRange = false;
            //print(enemyInRange);
        }
    }

    void Damage(Collider enemy)
    {
        EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {

            if (playerMacho.isMacho == true)
            {
                damageMulti = 2;
            }
            else
            {
                damageMulti = 1;
            }

            enemyHealth.TakeDamage(damagePerHit * damageMulti);
        }
    }

    void Animating()
    {
        anim.SetBool("IsHitting", hitting);
        hitting = false;
    }
}
