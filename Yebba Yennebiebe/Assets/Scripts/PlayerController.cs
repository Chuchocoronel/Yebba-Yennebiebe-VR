using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GemManager gemManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "FireGem_Spawner")
        {
            gemManager.SpawnGem(GemManager.type.FIREGEM);
        }
        else if (other.name == "WaterGem_Spawner")
        {
            gemManager.SpawnGem(GemManager.type.WATERGEM);
        }
        else if (other.name == "ElectricGem_Spawner")
        {
            gemManager.SpawnGem(GemManager.type.ELECTRICGEM);
        }
    }
}
