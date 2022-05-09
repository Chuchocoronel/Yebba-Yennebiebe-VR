using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    NavMeshAgent agent;

    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.stoppingDistance = 6.0f;
        agent.destination = target.transform.position;
    }
}
