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
    private UIManager uiMan;

    // Potions to drop
    public GameObject[] potions;

    bool dead = false;

    private float countdownDie = 4.0f;

    // Start is called before the first frame update
    public void Start()
    {
        movement = GetComponent<EnemyMovement>();
        anim = GetComponent<Animator>();
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<PlayerController>();
        uiMan = GameObject.Find("UI").GetComponent<UIManager>();
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
                    float randSpawn = Random.Range(0.0f, 100.0f);
                    if (randSpawn >= 90.0f)
                    {
                        int randPotion = Random.Range(0, 3);
                        GameObject.Instantiate(potions[randPotion], this.transform.position, Quaternion.identity);
                    }

                    GameObject.Instantiate(particles, this.transform);
                    mesh.SetActive(false);
                    deadOnce = true;

                    // Borrar este codigo y ponerlo en el nuevo sitio si se decide poner en otro sitio cuando se destruye un enemigo
                    switch (mesh.name)
                    {
                        case "Object02":
                            uiMan.points += 100;
                            break;
                        case "ChestMesh":
                            uiMan.points += 50;
                            break;
                    }

                    Destroy(this.gameObject, 5.0f);
                }
            }
            return;
        }
        
        
        if ((this.gameObject.transform.position - player.transform.position).magnitude <= 1.5f && attackCooldown <= 0.0f)
        {
            // TODO: Play hit animation?
            anim.SetTrigger("Attack");
            playerScript.hitPoints--;
            playerScript.regenerationCooldown = 4.0f;
            attackCooldown = 3.0f;
            if (!playerScript.dead && playerScript.hitPoints <= 0)
            {
                //playerScript.PlayerDead();
            }
        }
        
        attackCooldown -= Time.deltaTime;
        //countdownDie -= Time.deltaTime;

        //if (countdownDie <= 0.0f) Die();
    }

    public void Die()
    {
        movement.dead = true;
        anim.SetTrigger("Dead");
        dead = true;
    }
}
