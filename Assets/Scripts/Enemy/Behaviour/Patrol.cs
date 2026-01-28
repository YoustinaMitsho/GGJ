using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour
{
    [SerializeField] Transform[] waypoints;
    [SerializeField] GameObject spawnPoint;
    [SerializeField] Transform player;
    [SerializeField] float speed = 2f;
    [SerializeField] float detectionRange = 10f;
    [SerializeField] AudioSource patrolSound;
    int currentWaypointIndex;
    NavMeshAgent agent;
    storm stormScript;

    // Start is called before the first frame update
    void Start()
    {
        currentWaypointIndex = 0;
        agent = gameObject.GetComponent<NavMeshAgent>();
        stormScript = gameObject.GetComponent<storm>();
        if (stormScript != null)
        {
            stormScript.enabled = false;
        }
    }

    private void FixedUpdate()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            patrolSound.Play();
            StartStorming();
            spawnPoint.GetComponent<Spawning>().canSpawn = true;
        }
        else
        {
            Patroling();
        }
    }

    void StartStorming()
    {
        stormScript.enabled = true;
        this.enabled = false;
    }
    void Patroling()
    {
        if (waypoints == null || waypoints.Length == 0) return;

        agent.speed = speed;

        if (agent.destination != waypoints[currentWaypointIndex].position)
        {
            agent.SetDestination(waypoints[currentWaypointIndex].position);
        }

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance + 0.1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            agent.SetDestination(waypoints[currentWaypointIndex].position);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
