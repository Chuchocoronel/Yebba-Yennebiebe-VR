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

    bool dead = false;

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
        if (!dead)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Dead") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                Destroy(this.gameObject);
            }
            return;
        }
        
        if ((this.gameObject.transform.position - player.transform.position).magnitude <= 1.5f && attackCooldown <= 0.0f)
        {
            // TODO: Play hit animation? ¿Si quieres nos vamos?
            anim.SetTrigger("Attack");
            playerScript.hitPoints--;
            if (playerScript.hitPoints <= 0)
            {
                playerScript.PlayerDead();
            }
        }

        attackCooldown -= Time.deltaTime;
    }

    public void Die()
    {
        anim.SetTrigger("Dead");
        dead = true;
    }
}
