using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject player;
    public UIManager uiManager;
        
    private float spawnRate = 5.0f;
    private int currEnemyQuantity = 0;
    private int round = 1;
    private int maxEnemiesRound = 0;
    private int enemiesKilled = 0;
    // private int currentEnemyCount = 0;
    //  private int maxEnemies = 10;


    private AudioSource audioComp;

    // Start is called before the first frame update
    void Start()
    {
        audioComp = GetComponent<AudioSource>();
        maxEnemiesRound = 7;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnRate <= 0.0f)
        {
            if (currEnemyQuantity < maxEnemiesRound) SpawnEnemy();
            spawnRate = 1.5f;
        }
        {
            //Debug.Log("Time left to spawn" + spawnRate);
            spawnRate -= Time.deltaTime;
        }
    }

    private void SpawnEnemy()
    {
        int randNum = Random.Range(0, 2);
        GameObject enemySpawned = Instantiate(enemies[randNum]);
        currEnemyQuantity++;
        do
        {
            float x = gameObject.transform.position.x;
            float z = gameObject.transform.position.z;
            enemySpawned.transform.position = new Vector3(Random.Range(x + 30.0f, x + 90.0f), 1.0f, Random.Range(z - 30.0f, z + 30.0f));
        } while ((enemySpawned.transform.position - player.transform.position).magnitude <= 20.0f);
    }

    public void EnemyKilled()
    {
        enemiesKilled++;
        if (enemiesKilled == maxEnemiesRound) EndRound();
    }

    public void EndRound()
    {
        round++;
        audioComp.Play();
        spawnRate = 10.0f;
        currEnemyQuantity = 0;
        maxEnemiesRound += 6;
        enemiesKilled = 0;
        uiManager.NewRound(round);
    }
}