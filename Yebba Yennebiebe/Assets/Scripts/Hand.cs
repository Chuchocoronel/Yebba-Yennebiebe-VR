using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public bool isLeft;
    public PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.name == "FireGem_Spawner")
        {
            player.GripButton(PlayerController.MagicType.FIRE, isLeft);
        }
        else if (collision.gameObject.name == "WaterGem_Spawner")
        {
            player.GripButton(PlayerController.MagicType.WATER, isLeft);
        }
        else if (collision.gameObject.name == "ElectricGem_Spawner")
        {
            player.GripButton(PlayerController.MagicType.ELECTRIC, isLeft);
        }
    }
}
