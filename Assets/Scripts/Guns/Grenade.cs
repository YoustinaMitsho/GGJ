using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Grenade : MonoBehaviour
{
    float timer = 2;
    float countdown;
    bool has_exploded = false;

    [SerializeField] GameObject explosion_effect;
    [SerializeField] int power = 10;
    [SerializeField] float radius = 5f;
    [SerializeField] int enemyDamage = 100;
    [SerializeField] float damageDelay = 0.8f;
    [SerializeField] CamShake camShake;

    void Start()
    {
        countdown = timer;
        camShake = Camera.main.GetComponent<CamShake>();
    }

    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0 && !has_exploded) Explode();
    }

    void Explode()
    {
        if (has_exploded) return;
        has_exploded = true;

        if (explosion_effect != null)
        {
            GameObject particule = Instantiate(explosion_effect, transform.position, transform.rotation);
            Destroy(particule, 1f);
        }

        Destroy(gameObject);

        Collider[] colliders = Physics.OverlapSphere(transform.position, 5f);

        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(10f, transform.position, 5f, 1f, ForceMode.Impulse);
            }

            NavMeshAgent agent = hit.GetComponent<NavMeshAgent>();
            if (agent != null)
            {
                Vector3 pushDir = (hit.transform.position - transform.position).normalized;
                float distance = Vector3.Distance(hit.transform.position, transform.position);
                float effect = Mathf.Clamp01(1 - (distance / 5f));
                agent.Move(pushDir * 10f * effect);
                StartCoroutine(camShake.Shake(0.3f, 0.4f));
            }
            hit.TryGetComponent<EnemyHealth>(out EnemyHealth enemyHealth);
            if (enemyHealth != null)
            {
                StartCoroutine(ApplyDamage());
                enemyHealth.TakeDamage(enemyDamage);
            }
            else
            {
                Debug.Log("No EnemyHealth component found on " + hit.name);
            }
        }
    }

    IEnumerator ApplyDamage()
    {
        yield return new WaitForSeconds(damageDelay);
    }
}