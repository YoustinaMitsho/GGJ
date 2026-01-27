using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyKnockback : MonoBehaviour
{
    private NavMeshAgent agent;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void ApplyKnockback(Vector3 velocity)
    {
        StopAllCoroutines(); // Reset if hit by multiple grenades
        StartCoroutine(KnockbackRoutine(velocity));
    }

    private IEnumerator KnockbackRoutine(Vector3 velocity)
    {
        float friction = 5f;
        while (velocity.magnitude > 0.1f)
        {
            if (agent == null || !agent.enabled) yield break;

            agent.Move(velocity * Time.deltaTime);
            velocity = Vector3.Lerp(velocity, Vector3.zero, Time.deltaTime * friction);
            yield return null;
        }
    }
}