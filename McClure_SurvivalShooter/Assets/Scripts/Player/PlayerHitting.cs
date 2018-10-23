using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitting : MonoBehaviour {

    public int damagePerHit = 20;
    //Macho mutiplier
    public int damageMulti = 1;

    public float timeBetweenHits = 1f;

    float timer;

    GameObject player;
    PlayerMacho playerMacho;
    Collider enemy = null;

    List<Collider> enemies;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerMacho = player.GetComponent<PlayerMacho>();

        enemies = new List<Collider>();
    }

    // Update is called once per frame
    void Update () {

        timer += Time.deltaTime;

        if (Input.GetButton("Fire1") && timer >= timeBetweenHits)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                Damage(enemies[i]);
            }
        }

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
}
