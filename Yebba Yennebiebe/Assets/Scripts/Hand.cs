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

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Fire")
        {
            player.GripButton(PlayerController.MagicType.FIRE, isLeft);
        }
        else if (other.gameObject.tag == "Ice")
        {
            player.GripButton(PlayerController.MagicType.ICE, isLeft);
        }
        else if (other.gameObject.tag == "Electric")
        {
            player.GripButton(PlayerController.MagicType.ELECTRIC, isLeft);
        }
    }
}
