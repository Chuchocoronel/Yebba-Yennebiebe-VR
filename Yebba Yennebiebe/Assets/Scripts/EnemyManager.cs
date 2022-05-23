using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemy;
    public GameObject player;
    private float spawnRate = 5.0f;
    private int spawnQuantity = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnRate <= 0.0f)
        {
            SpawnEnemy();
            spawnRate = 5.0f;
        }
        {
            //Debug.Log("Time left to spawn" + spawnRate);
            spawnRate -= Time.deltaTime;
        }
    }

    private void SpawnEnemy()
    {
        for (int i = 0; i < spawnQuantity; ++i)
        {
            GameObject enemySpawned = Instantiate(enemy);
            do
            {
                float x = gameObject.transform.position.x;
                float z = gameObject.transform.position.z;
                enemySpawned.transform.position = new Vector3(Random.Range(x - 30.0f, x + 30.0f), 1.0f, Random.Range(z - 30.0f, z + 30.0f));
            } while ((enemySpawned.transform.position - player.transform.position).magnitude <= 20.0f);
        }
    }
}