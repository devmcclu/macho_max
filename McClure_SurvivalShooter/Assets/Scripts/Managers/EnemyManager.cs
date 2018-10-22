using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public GameObject enemy;
    public float spawnTime; // = Random.Range(3.0f, 20.0f);//3f;
    public Transform[] spawnPoints;
    //float number = Random.Range(3.0f, 20.0f);


    void Start ()
    {
        spawnTime = spawnTime = Random.Range(3.0f, 20.0f);
        InvokeRepeating ("Spawn", spawnTime, spawnTime);
    }


    void Spawn ()
    {


        if(playerHealth.currentHealth <= 0f)
        {
            return;
        }

        int spawnPointIndex = Random.Range (0, spawnPoints.Length);

        Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }
}
