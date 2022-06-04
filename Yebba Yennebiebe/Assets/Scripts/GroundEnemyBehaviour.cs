using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemyBehaviour : MonoBehaviour
{
    public int hitNum = 1;
    private float attackCooldown = 3.0f;
 
    private GameObject player;
    private PlayerController playerScript;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((this.gameObject.transform.position - player.transform.position).magnitude <= 1.5f && attackCooldown <= 0.0f)
        {
            // TODO: Play hit animation?
            anim.SetTrigger("Attack");
            playerScript.hitPoints--;
            if (playerScript.hitPoints <= 0)
            {
                playerScript.PlayerDead();
            }
        }

        attackCooldown -= Time.deltaTime;
    }
}
