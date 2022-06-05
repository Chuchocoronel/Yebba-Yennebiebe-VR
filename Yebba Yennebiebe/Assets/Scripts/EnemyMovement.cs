using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    NavMeshAgent agent;
    public bool dead = false;

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
        if (agent.destination != target.transform.position && !dead)
        {
            agent.destination = target.transform.position;
        }
        else if (dead)
        {
            agent.isStopped = true;
        }
    }
}
