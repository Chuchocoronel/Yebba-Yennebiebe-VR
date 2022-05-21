using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemManager : MonoBehaviour
{
    public enum type
    {
        FIREGEM = 0,
        WATERGEM,
        ELECTRICGEM
    }
    public Transform fireSpawner;
    public Transform waterSpawner;
    public Transform electricSpawner;

    public GameObject fireGem;
    public GameObject waterGem;
    public GameObject electricGem;

    public float spawnTime = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        SpawnGem(type.FIREGEM);
        SpawnGem(type.WATERGEM);
        SpawnGem(type.ELECTRICGEM);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SpawnGem(type gem)
    {
        switch (gem)
        {
            case type.FIREGEM:
                Instantiate(fireGem, fireSpawner);
                break;
            case type.WATERGEM:
                Instantiate(waterGem, waterSpawner);
                break;
            case type.ELECTRICGEM:
                Instantiate(electricGem, electricSpawner);
                break;
            default:
                break;
        }
    }
}
