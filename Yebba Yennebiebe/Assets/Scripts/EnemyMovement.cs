using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    NavMeshAgent agent;
    public bool dead = false;
    public bool stunned = false;

    private float stunCooldown = 1.0f;

    private GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player");
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.stoppingDistance = 1.5f;
        agent.destination = target.transform.position;
    }

    private void Update()
    {
        if (agent.destination != target.transform.position && !dead && !stunned)
        {
            agent.destination = target.transform.position;
        }
        else if (dead || stunned)
        {
            agent.isStopped = true;
            agent.speed = 0.0f;
            agent.velocity = Vector3.zero;
        }

        if (stunned)
        {
            stunCooldown -= Time.deltaTime;
            if (stunCooldown <= 0.0f)
            {
                stunned = false;
                stunCooldown = 1.0f;
                agent.isStopped = false;
                agent.speed = 6.0f;
            }
        }
    }
}
