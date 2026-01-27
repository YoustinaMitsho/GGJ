using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class storm : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform fortress;

    [Header("Behavior Settings")]
    [SerializeField] float stopChasingRange = 15f;
    [SerializeField] bool canReturnToPatrol = false;

    Patrol patrolScript;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        patrolScript = GetComponent<Patrol>();

        if (patrolScript == null)
        {
            canReturnToPatrol = false;
        }
    }

    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
            fortress = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (fortress == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, fortress.position);

        if (canReturnToPatrol && distanceToPlayer > stopChasingRange)
        {
            StopStorming();
        }
        else
        {
            agent.SetDestination(fortress.position);
        }
    }

    void StopStorming()
    {
        agent.ResetPath();
        if (patrolScript != null)
        {
            patrolScript.enabled = true;
            this.enabled = false;
        }
    }
}