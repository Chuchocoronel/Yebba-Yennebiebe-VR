using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemyBehaviour : MonoBehaviour
{
    public int hitNum = 1;
 
    private GameObject player;
    private PlayerController playerScript;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((this.gameObject.transform.position - player.transform.position).magnitude <= 1.0f)
        {
            // TODO: Play hit animation?
            playerScript.hitPoints--;
            if (playerScript.hitPoints <= 0)
            {
                playerScript.PlayerDead();
            }
        }
    }
}
