using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class GroundEnemyBehaviour : MonoBehaviour
{
    public int hitNum = 1;
    private float attackCooldown = 3.0f;
    private bool deadOnce = false;

    public EnemyManager manager;
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

    public AudioSource source;

    public float life = 10.0f;

    private bool iceStun;
    private float iceCooldown = 2.0f;

    private float fireCooldown = 2.0f;
    private bool fireStun;

    // Start is called before the first frame update
    public void Start()
    {
        manager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
        movement = GetComponent<EnemyMovement>();
        anim = GetComponent<Animator>();
        player = GameObject.Find("Player");
        playerScript = player.GetComponent<PlayerController>();
        uiMan = GameObject.Find("Canvas").GetComponent<UIManager>();
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
                            ScorePoints.totalPoints += 100;
                            break;
                        case "ChestMesh":
                            ScorePoints.totalPoints += 50;
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
            // Handling managed in player script when get damaged
            playerScript.GetHurt(1);
            attackCooldown = 3.0f;
        }
        
        attackCooldown -= Time.deltaTime;

        if (iceStun)
        {
            iceCooldown -= Time.deltaTime;
            if (iceCooldown <= 0.0f)
            {
                iceCooldown = 2.0f;
                iceStun = false;
                GetComponent<NavMeshAgent>().speed = 6.0f;
            }
        }

        if (fireStun)
        {
            fireCooldown -= Time.deltaTime;
            life -= Time.deltaTime * 0.25f;
            if (fireCooldown <= 0.0f)
            {
                fireCooldown = 2.0f;
                fireStun = false;
            }
        }
        //Uncomment this to kill enemies in 4 s
        //if (!dead)
        //{
        //    countdownDie -= Time.deltaTime;
        //    if (countdownDie <= 0.0f) Die();
        //}
    }

    public void Die(string type)
    {
        if (dead) return;

        switch(type)
        {
            case "Fire":
                fireStun = true;
                life -= 4.0f;
                break;
            case "Ice":
                GetComponent<NavMeshAgent>().speed = 2.5f;
                iceStun = true;
                life -= 2.5f;
                break;
            case "Electric":
                GetComponent<EnemyMovement>().stunned = true;
                life -= 1.0f;
                break;
            case "Default":
                life -= 1000.0f;
                break;
        }

        if (life <= 0.0f)
        {
            source.Play();
            movement.dead = true;
            anim.SetTrigger("Dead");
            GetComponent<BoxCollider>().enabled = false;
            dead = true;
            manager.EnemyKilled();
        }
    }
}
