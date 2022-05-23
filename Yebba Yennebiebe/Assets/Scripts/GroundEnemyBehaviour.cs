using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemyBehaviour : MonoBehaviour
{
    public int hitNum = 1;
    public Animator anim;
    private GameObject player;
    private float cooldown = 3.5f;
    private PlayerController playerScript;
    

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<PlayerController>();
        Physics.IgnoreLayerCollision(6, 6, true);

    }

    // Update is called once per frame
    void Update()
    {
        cooldown -= Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && cooldown <= 0.0f)
        {
            anim.SetTrigger("Attack");
            // TODO: Play hit animation?
            playerScript.hitPoints--;
            if (playerScript.hitPoints <= 0)
            {
                playerScript.PlayerDead();
            }
            cooldown = 3.5f;
        }
    }
}
