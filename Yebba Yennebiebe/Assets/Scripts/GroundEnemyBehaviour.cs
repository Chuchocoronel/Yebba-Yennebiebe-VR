using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class GroundEnemyBehaviour : MonoBehaviour
{
    public int hitNum = 1;
    private float attackCooldown = 3.0f;
    private bool deadOnce = false;

    public GameObject particles;
    private GameObject player;
    private PlayerController playerScript;
    private EnemyMovement movement;
    public GameObject mesh;
    private Animator anim;

    bool dead = false;

    private float countdownDie = 4.0f;

    // Start is called before the first frame update
    public void Start()
    {
        movement = GetComponent<EnemyMovement>();
        anim = GetComponent<Animator>();
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    public void Update()
    {
        if (dead)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                if (!deadOnce)
                {
                    GameObject.Instantiate(particles, this.transform);
                    mesh.SetActive(false);
                    deadOnce = true;
                    Destroy(this.gameObject, 5.0f);
                }
            }
            return;
        }
        
        if ((this.gameObject.transform.position - player.transform.position).magnitude <= 1.5f && attackCooldown <= 0.0f)
        {
            // TODO: Play hit animation? �Si quieres nos vamos?
            anim.SetTrigger("Attack");
            playerScript.hitPoints--;
            playerScript.regenerationCooldown = 4.0f;
            if (playerScript.hitPoints <= 0)
            {
                playerScript.PlayerDead();
            }
        }

        attackCooldown -= Time.deltaTime;

        countdownDie -= Time.deltaTime;

        if (countdownDie <= 0.0f) Die();
    }

    public void Die()
    {
        movement.dead = true;
        anim.SetTrigger("Dead");
        dead = true;
    }
}
